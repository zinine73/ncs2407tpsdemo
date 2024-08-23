using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            ContactPoint cp = collision.GetContact(0);
            // �浹�� �Ѿ��� �������͸� ���ʹϾ����� ��ȯ
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spark = Instantiate(sparkEffect, cp.point, rot);

            Destroy(spark, 0.5f);

            Destroy(collision.gameObject);
        }
    }
}
