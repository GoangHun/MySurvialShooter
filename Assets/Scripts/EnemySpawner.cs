using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour {
    public Enemy[] enemyPrefabs; // 생성할 적 AI

    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() 
    {
        // 플레이어 체력 UI 갱신
    }

    private IEnumerator CreateEnemy()
    {
        while(!GameManager.instance.isGameover)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            var enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], point.position, point.rotation);
            enemies.Add(enemy);

            enemy.onDeath += () =>
            {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject, 3f);
                GameManager.instance.AddScore(10);
            };

            yield return new WaitForSeconds(2f);
        }  
    }
}