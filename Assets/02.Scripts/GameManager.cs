using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 몬스터가 출현할 위치를 저장할 List
    public List<Transform> points = new List<Transform>();

    void Start()
    {
        // SpawnPointGroup의 Transform 추출
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // SpawnPointGroup 하위에 있는 모든 차일드 오브젝트의 Transform 추출
        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
    }
}
