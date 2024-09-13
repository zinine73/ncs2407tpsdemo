using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    delegate float SumHandler(float a, float b);
    SumHandler sumHandler;

    float Sum(float a, float b)
    {
        return a + b;
    }

    float Minus(float a, float b)
    {
        return a - b;
    }

    void Start()
    {
        sumHandler = Sum;
        float sum = sumHandler(10.0f, 5.0f);
        Debug.Log($"Sum = {sum}");
        sumHandler = Minus;
        float minus = sumHandler(10.0f, 5.0f);
        Debug.Log($"Minus = {minus}");
        sumHandler += Sum;
        sumHandler += Minus;

        // 델리게이트 변수에 람다식 선언
        sumHandler = (float a, float b) => (a + b);
        float sum2 = sumHandler(10.0f, 5.0f);
        Debug.Log($"Sum2 = {sum2}");

        // 델리게이트 변수에 무명 메서드 연결
        sumHandler = delegate (float a, float b) { return a + b; };
        float sum3 = sumHandler(2.0f, 3.0f);
        Debug.Log($"Sum3 = {sum3}");

        sumHandler = (float x, float y) => (x - y);
    }
}
