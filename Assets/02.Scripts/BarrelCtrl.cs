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

        rb.mass = 1.0f;
        // ���� �ڱ�ġ�� ���� ���Ѵ�
        rb.AddForce(Vector3.up * 1500.0f);

        Destroy(gameObject, 3.0f);
    }
}
