using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    #region Public
    public GameObject expEffect;
    // �������� ������ �ؽ��� �迭
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

        // ������ �ִ� MeshRenderer ����
        renderer = GetComponentInChildren<MeshRenderer>();

        int idx = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[idx];
    }

    /// <summary>
    /// �浹 �� �߻��ϴ� �ݹ��Լ�
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
        // ���� ȿ�� ��ƼŬ ���� ����
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);

        // ���� ȿ�� ��ƼŬ 5�� �Ŀ� ����
        Destroy(exp, 5.0f);

        //rb.mass = 1.0f;
        // ���� �ڱ�ġ�� ���� ���Ѵ�
        //rb.AddForce(Vector3.up * 1500.0f);

        // ���� ���߷� ����
        IndirectDamage(tr.position);

        Destroy(gameObject, 3.0f);
    }

    void IndirectDamage(Vector3 pos)
    {
        // �ֺ��� �ִ� �巳���� ��� ���� (GC�� �߻�)
        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        // GC�߻����� ����
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
