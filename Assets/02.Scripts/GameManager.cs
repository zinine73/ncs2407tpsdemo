using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 몬스터가 출현할 위치를 저장할 List
    public List<Transform> points = new List<Transform>();

    public GameObject monster;
    public float createTime = 3.0f;
    private bool isGameOver;
    public bool IsGameOver //isGameOver의 property
    {
        get => isGameOver; //get{ return isGameOver; }
        set{
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    // 싱글턴 인스턴스 선언
    public static GameManager instance = null;

    private void Awake()
    {
        // instance가 할당되지 않은 경우
        if (instance == null)
        {
            instance = this;
        }
        // instance에 할당된 클래스의 인스턴스가 다를 경우
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        // 다른 scene으로 넘어가더라도 삭제하지 않고 유지함
        DontDestroyOnLoad(this.gameObject);
    }

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

        // 일정한 시간 간격으로 함수 호출
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    private void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }
}
