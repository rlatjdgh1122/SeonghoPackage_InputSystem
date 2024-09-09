using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seongho.InputSystem
{
    public class InputMachine<T> : IInputHandler<T> where T : Enum
    {
        private Dictionary<T, InputParams> _inputEventDic = new();                        //EnumŸ���� ���� �̺�Ʈ�� ����
        private Dictionary<T, INPUT_KEY_STATE> _inputStateDic = new();                    //EnumŸ���� ���� Ű�ǻ��� ���� 
        private Dictionary<T, Coroutine> _inputCoroutineDic = new();                      //EnumŸ���� ����   �ڷ�ƾ ���� 

        /// <summary>
        /// �ε����� Ű���¸� �����ɴϴ�.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public INPUT_KEY_STATE this[T key]
        {
            get
            {
                return _inputStateDic[key];
            }
        }

        /// <summary>
        /// Key�߰� or Action�߰�
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void OnRegisterEvent(T key, InputParams action)
        {
            if (!_inputEventDic.ContainsKey(key))
            {

                _inputEventDic.Add(key, action);

                //ó���� NOT_PRESSED���·� �ʱ�ȭ
                _inputStateDic.Add(key, INPUT_KEY_STATE.NOT_PRESSED);
            }
            else
            {

                _inputEventDic[key] += action;
            }

        }

        /// <summary>
        /// �׼� ����
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void RemoveRegisterEvent(T key, InputParams action)
        {

            if (_inputEventDic[key] == null)
            {
                _inputEventDic.Remove(key);
                _inputStateDic.Remove(key);
                _inputCoroutineDic.Remove(key);
            }
            else
            {
                _inputEventDic[key] -= action;
            }

        }

        /// <summary>
        /// ������ ��ǲ�� ó��
        /// </summary>
        /// <param name="type">��ǲ Ÿ��</param>
        /// <param name="context"></param> 
        public void InputRunning(T type, InputAction.CallbackContext context, bool useHolding = false, params object[] @parmas)
        {
            //���� ���ٸ� �������ݴϴ�.
            if (!_inputEventDic.ContainsKey(type)) return;

            //�Է��� �޾Ҵٸ� Down���·� �Ѿ�ϴ�.
            if (context.performed)
            {
                _inputStateDic[type] = INPUT_KEY_STATE.DOWN;
                _inputEventDic[type]?.Invoke(INPUT_KEY_STATE.DOWN, @parmas);

                if (useHolding) //������ �ִ����� üũ�Ѵٸ�
                {
                    //CallWaitForStopCorouine�� ����Ͽ� Ű�ǻ��°� Up�� �ɶ����� PRESSING���·� �Ѱ��ݴϴ�.
                    //�ڷ�ƾ�� ��ȯ�Ͽ� �����մϴ�.
                    Coroutine corou = CoroutineUtil.CallWaitForStopCorouine(
                            () =>
                            {

                                _inputStateDic[type] = INPUT_KEY_STATE.PRESSING;
                                _inputEventDic[type]?.Invoke(INPUT_KEY_STATE.PRESSING, @parmas);

                            },
                            0.02f
                        );

                    //�ڷ�ƾ�� �����մϴ�.
                    _inputCoroutineDic.Add(type, corou);
                }


            } //end performed

            if (context.canceled)
            {
                //���� Ű�Է��� ��ҵƴٸ� Up���·� �Ѿ�ϴ�.
                _inputStateDic[type] = INPUT_KEY_STATE.UP;
                _inputEventDic[type]?.Invoke(INPUT_KEY_STATE.UP, @parmas);

                if (useHolding) //������ �ִ����� üũ�Ѵٸ�
                {
                    //������ �ڷ�ƾ�� ������ ��� �����ݴϴ�.
                    if (_inputCoroutineDic.TryGetValue(type, out var corou))
                    {
                        CoroutineUtil.StopCoroutine(corou);
                        _inputCoroutineDic.Remove(type);
                    }
                }

            } //end canceled

        } // end method
    } // end class
} //end namespace
