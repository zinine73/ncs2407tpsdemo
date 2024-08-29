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
        muzzleFlash.enabled = true;

        // 0.2�� ���� ����ϴ� ���� �޽��� ������ ����� �纸
        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;
    }
}
