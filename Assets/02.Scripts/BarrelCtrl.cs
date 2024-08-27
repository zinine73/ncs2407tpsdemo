using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    #region Public
    public GameObject expEffect;
    // 무작위로 적용할 텍스쳐 배열
    public Texture[] textures;
    public float radius = 10.0f;
    #endregion

    #region private
    private new MeshRenderer renderer;
    Transform tr;
    Rigidbody rb;

    int hitCount = 0;
    Collider[] colls = new Collider[10];
    #endregion

    void Start()
    {
        tr = GetComponent<Transform>();
        //rb = GetComponent<Rigidbody>();

        // 하위에 있는 MeshRenderer 추출
        renderer = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[idx];
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

        //rb.mass = 1.0f;
        // 위로 솟구치는 힘을 가한다
        //rb.AddForce(Vector3.up * 1500.0f);

        // 간접 폭발력 전달
        IndirectDamage(tr.position);

        Destroy(gameObject, 3.0f);
    }

    void IndirectDamage(Vector3 pos)
    {
        // 주변에 있는 드럼통을 모두 추출 (GC가 발생)
        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        // GC발생하지 않음
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);

        foreach (var coll in colls)
        {
            if (coll != null)
            {
                rb = coll.GetComponent<Rigidbody>();
                rb.mass = 1.0f;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
            }
        }
    }
}
