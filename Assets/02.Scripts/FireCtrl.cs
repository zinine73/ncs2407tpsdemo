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

        // FirePos 하위에 있는 MuzzleFlash의 Material 컴포넌트 추출
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
        // Bullet prefab을 동적으로 생성 (생성할 객체, 위치, 회전)
        Instantiate(bullet, firePos.position, firePos.rotation);

        audio.PlayOneShot(fireSfx, 1.0f);

        // 총구 화염 효과 코루틴 함수 호출
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.enabled = true;

        // 0.2초 동안 대기하는 동안 메시지 루프로 제어권 양보
        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;
    }
}
