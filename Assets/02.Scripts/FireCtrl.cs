using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public AudioClip fireSfx;

    private new AudioSource audio;
    private MeshRenderer muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        // FirePos ������ �ִ� MuzzleFlash�� Material ������Ʈ ����
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();

        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet prefab�� �������� ���� (������ ��ü, ��ġ, ȸ��)
        Instantiate(bullet, firePos.position, firePos.rotation);

        audio.PlayOneShot(fireSfx, 1.0f);

        // �ѱ� ȭ�� ȿ�� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        // ������ ��ǥ���� �����Լ��� ����
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        // int���� Random�϶��� max ���� �������� �ʴ´�
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        // float���� Random�϶��� max ������ �����Ѵ�
        float scale = Random.Range(1.0f, 1.5f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        muzzleFlash.enabled = true;

        // 0.2�� ���� ����ϴ� ���� �޽��� ������ ����� �纸
        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;
    }
}
