namespace Seongho.InputSystem
{
    // Ű �Է� ���¸� ��Ÿ���� ������ (�޸� ������ ���� byte�� ����)
    public enum INPUT_KEY_STATE : byte
    {
        NOT_PRESSED = 0,  // Ű�� ������ ����
        DOWN,             // Ű�� ����
        PRESSING,         // Ű�� ��� �����ִ� ����
        UP,               // Ű�� ������
    }
}