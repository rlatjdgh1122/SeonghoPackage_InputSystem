using System;

namespace Seongho.InputSystem
{
    public static partial class InputManager
    {

        public static PlayerAction Input = null;

        static InputManager()
        {
            Input = new();
        }

        /// <summary>
        /// �������� InputMachine�� �������ݴϴ�.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputMachine"></param>
        public static void CreateMachine<T>(out InputMachine<T> inputMachine) where T : Enum
        {
            inputMachine = new();
        }
    }

}

