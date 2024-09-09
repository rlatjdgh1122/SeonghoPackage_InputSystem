
using System;
using System.Collections;
using UnityEngine;

public static partial class CoroutineUtil
{
    /// <summary>
    /// ������ �����Ҷ����� �����մϴ�.
    /// </summary>
    /// <param name="predicate"> ���� ���� </param>
    /// <param name="action"> �����Ǹ鼭 ������ Action </param>
    /// <param name="heartBeat"> �ݺ� �ֱ� </param>
    /// <param name="afterAction"> ���� �� ������ Action</param>
    public static void CallWaitForActionUntilTrue(Func<bool> predicate, Action action, float heartBeat = 0.02f, Action afterAction = null)
    {
        EnsureCoroutineExecutor();

        _coroutineExecutor.StartCoroutine(DoCallWaitForActionUntilTrue(heartBeat, predicate, action, afterAction));
    }

    /// <summary>
    /// ���� Stop�Ҷ����� �����մϴ�.
    /// CorouineUtil.StopCoroutine�� ����Ͽ� �ܺο��� �������ݴϴ�.
    /// </summary>
    /// <param name="action"> �����Ǹ鼭 ������ Action </param>
    /// <param name="heartBeat"> �ݺ� �ֱ� </param>
    /// <returns> �ڷ�ƾ </returns>
    public static Coroutine CallWaitForStopCorouine(Action action, float heartBeat = 0.02f)
    {
        EnsureCoroutineExecutor();

        return _coroutineExecutor.StartCoroutine(DoCallWaitForStopCorouine(heartBeat, action));
    }

    /// <summary>
    /// �ڷ�ƾ�� ����ϴ�.
    /// CallWaitForStopCorouine�� ȣȯ�˴ϴ�.
    /// </summary>
    /// <param name="coroutine"> �������� �� �ڷ�ƾ </param>
    /// <param name="afterAction"> ���� �� ������ Action </param>
    public static void StopCoroutine(Coroutine coroutine, Action afterAction = null)
    {
        _coroutineExecutor.StopCoroutine(coroutine);
        afterAction?.Invoke();
    }

    private static IEnumerator DoCallWaitForActionUntilTrue(float heartBeat, Func<bool> predicate, Action action, Action afterAction)
    {
        while (!predicate())
        {
            yield return new WaitForSeconds(heartBeat);
            action?.Invoke();
        }

        afterAction?.Invoke();
    }

    private static IEnumerator DoCallWaitForStopCorouine(float heartBeat, Action action)
    {
        while (true)
        {
            yield return new WaitForSeconds(heartBeat);
            action?.Invoke();
        }
    }
}


