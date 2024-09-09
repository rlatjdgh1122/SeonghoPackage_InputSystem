using UnityEngine;
using UnityEngine.InputSystem;

namespace Seongho.InputSystem
{
    public class TestPlayerInput : MonoBehaviour, IPlayerInput
    {

        private InputMachine<HASH_INPUT_PLAYER> _inputContainer = null;

        private void Awake()
        {
            //InputMachine�� �������ݴϴ�.
            InputManager.CreateMachine(out _inputContainer);

            InputSetting();
        }

        /// <summary>
        /// InputAction�� �������ݴϴ�.
        /// </summary>
        public void InputSetting()
        {
            InputManager.Input.Player.SetCallbacks(this);
        }

        /// <summary>
        /// �̺�Ʈ���� HashKey�� ���� �������ݴϴ�.
        /// </summary>
        public void OnRegisterEvent(HASH_INPUT_PLAYER key, InputParams action)
        {
            _inputContainer.OnRegisterEvent(key, action);
        }

        /// <summary>
        /// �̺�Ʈ���� HashKey�� ���� �������ݴϴ�.
        /// </summary>
        public void RemoveRegisterEvent(HASH_INPUT_PLAYER key, InputParams action)
        {
            _inputContainer.RemoveRegisterEvent(key, action);
        }

        /// <summary>
        /// Ű�� �������ݴϴ�.
        /// </summary>
        public void OnLeftClickInput(InputAction.CallbackContext context)
        {
            //���� Ŭ���� ���� ��� ����� �̺�Ʈ ����

            //�Ķ���� ����
            // 1. ������ HashKey
            // 2. context
            // 3. InputHolding�� ���ٰ����� �����ٰ������� ���� ����
            // 4. ���� �Ѱ��� params object - �������� object�� �Ѱ��� �� ���� (���������)
            // *���� : InputHolding�� ������� ���� ��쿣 false�� ���ִ� ���� ���ɻ����� ����.
            _inputContainer.InputRunning(HASH_INPUT_PLAYER.LeftClick, context, true);

        }

        public void OnSpaceClickInput(InputAction.CallbackContext context)
        {
            //�����̽� �ٸ� ���� ��� ����� �̺�Ʈ ����

            _inputContainer.InputRunning(HASH_INPUT_PLAYER.Space, context, false);
        }
    }
}