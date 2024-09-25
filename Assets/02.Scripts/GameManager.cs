using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // 몬스터가 출현할 위치를 저장할 List
    public List<Transform> points = new List<Transform>();

    // 몬스터를 미리 생성해 저장할 리스트
    public List<GameObject> monsterPool = new List<GameObject>();

    // 오브젝트 풀(Object Pool) 에 생성할 몬스터의 최대 갯수
    public int maxMonsters = 10;

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

    [SerializeField] private TMP_Text scoreText;
    private int totScore = 0;

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

    private void Start()
    {
        // 몬스터 오브젝트 풀 생성
        CreateMonsterPool();

        // SpawnPointGroup의 Transform 추출
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // SpawnPointGroup 하위에 있는 모든 차일드 오브젝트의 Transform 추출
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }

        // 일정한 시간 간격으로 함수 호출
        InvokeRepeating("CreateMonster", 2.0f, createTime);

        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DisplayScore(0);
    }

    private void CreateMonster()
    {
        // 몬스터의 불규칙한 생성 위치 산출
        int idx = Random.Range(0, points.Count);
        // 오브젝트 풀에서 몬스터 추출
        GameObject _monster = GetMonsterInPool();
        // 추출한 몬스터의 위치와 회전을 설정
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        // 몬스터 활성화
        _monster?.SetActive(true);
    }

    private void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            // 몬스터 생성
            var _monster = Instantiate<GameObject>(monster);
            // 몬스터 이름을 지정
            _monster.name = $"Monster_{i:00}";
            //_monster.name = "Monster_" + i.ToString("00");
            // 몬스터 비활성화
            _monster.SetActive(false);
            // 생성한 몬스터를 오브젝트 풀에 추가
            monsterPool.Add(_monster);
        }
    }

    public GameObject GetMonsterInPool()
    {
        // 오브젝트 풀의 처음부터 끝까지 순회
        foreach (var _monster in monsterPool)
        {
            // 비활성화 여부로 사용 가능한 몬스터를 판단
            if (_monster.activeSelf == false)
            {
                // 몬스터 반환
                return _monster;
            }
        }
        return null;
    }

    /// <summary>
    /// 점수를 누적하고 출력하는 함수
    /// </summary>
    /// <param name="score">누적할 점수</param>
    public void DisplayScore(int score)
    {
        totScore += score;
        scoreText.text = $"<color=#00ff00>SCORE : </color><color=#ff0000>{totScore:#,##0}</color>";
        // 스코어 저장
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }

#if UNITY_EDITOR
    [MenuItem("SpaceShooter/Reset Score", false, 1)]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
