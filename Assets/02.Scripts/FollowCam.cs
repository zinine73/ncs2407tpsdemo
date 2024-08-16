using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라가야할 대상을 연결할 변수
    public Transform targetTr;
    // 카메라 자신의 transform
    private Transform camTr;

    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    public float damping = 10.0f;
    public float targetOffset = 2.0f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = targetTr.position
                        + (-targetTr.forward * distance)
                        + (Vector3.up * height);

        // 구면선형보간을 이용한 방법
        //camTr.position = Vector3.Slerp(camTr.position,
        //                                pos,
        //                                Time.deltaTime * damping);

        camTr.position = Vector3.SmoothDamp(camTr.position,
                                            pos,
                                            ref velocity,
                                            damping);

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
