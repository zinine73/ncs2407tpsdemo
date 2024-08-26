using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    #region Public
    public GameObject expEffect;
    #endregion

    #region private
    Transform tr;
    Rigidbody rb;

    int hitCount = 0;
    #endregion

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 충돌 시 발생하는 콜백함수
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision) 
    { 
        if (collision.collider.CompareTag("BULLET")) 
        {
            if (++hitCount == 3) 
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        // 폭발 효과 파티클 동적 생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);

        // 폭발 효과 파티클 5초 후에 제거
        Destroy(exp, 5.0f);

        rb.mass = 1.0f;
        // 위로 솟구치는 힘을 가한다
        rb.AddForce(Vector3.up * 1500.0f);

        Destroy(gameObject, 3.0f);
    }
}
