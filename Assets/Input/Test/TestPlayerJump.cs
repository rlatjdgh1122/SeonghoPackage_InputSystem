using UnityEngine;

namespace Seongho.InputSystem
{
    public class TestPlayerJump : MonoBehaviour
    {
        [Range(5f, 15f)]
        public float JumpPower = 10f;

        private IInputHandler<HASH_INPUT_PLAYER> _input = null;
        private Rigidbody2D _rb = null;

        private void Awake()
        {
            _input = GetComponent<IInputHandler<HASH_INPUT_PLAYER>>();
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// �̺�Ʈ�� �������ݴϴ�.
        /// </summary>
        private void Start()
        {
            _input.OnRegisterEvent(HASH_INPUT_PLAYER.Space, OnJump);
        }

        /// <summary>
        /// �̺�Ʈ�� ��ü���ݴϴ�.
        /// </summary>
        private void OnDestroy()
        {
            _input.RemoveRegisterEvent(HASH_INPUT_PLAYER.Space, OnJump);
        }

        private void OnJump(INPUT_KEY_STATE key, object[] args)
        {
            //������ ���
            if (key == INPUT_KEY_STATE.DOWN)
            {
                //���� ���� ���� ���� ����
                _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
        }

    }
}