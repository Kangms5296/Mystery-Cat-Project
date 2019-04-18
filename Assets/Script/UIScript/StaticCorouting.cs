using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCorouting : MonoBehaviour {

    // 전역 사용을 위한 static 변수
    private static StaticCorouting sc;

    // 각 코루틴 제어를 위한 변수
    private static Dictionary<string, IEnumerator> coroutines;

    private void Awake()
    {
        sc = this;
        coroutines = new Dictionary<string, IEnumerator>();
    }

    public static void Start(string coroutineName, IEnumerator routine)
    {
        // 해당 코루틴 이름의 코루틴이 기존에 등록되어있지 않으면 새로 등록
        if (coroutines.ContainsKey(coroutineName) == false)
            coroutines.Add(coroutineName, routine);

        coroutines[coroutineName] = routine;
        sc.StartCoroutine(coroutines[coroutineName]);
    }

    public static void Stop(string coroutineName)
    {
        if (coroutines.ContainsKey(coroutineName) == false)
            return;

        if (coroutines[coroutineName] != null)
            sc.StopCoroutine(coroutines[coroutineName]);
    }
}
