﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject boss;
    GameObject spawnNode;
    public GameObject endNode;
    public GameObject enemy;
    GameObject lastEnemy;
    Vector3 offset = new Vector3(0, 0.4f, 0);

    [Header("Spawn Stuff")]
    private float countDown = 2f;
    public float timeBetweenWaves = 5f;
    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI levelCount;
    private int waveIndex = 0;
    public int level = 0;
    float enemyHP = 50;
    float bossHP = 2000;


    // Start is called before the first frame update
    void Start()
    {
        spawnNode = GameObject.FindGameObjectWithTag("startNode");
        spawnNode.GetComponent<Renderer>().material.color = Color.grey;
        spawnNode.GetComponent<NodeScript>().unbuildable = true;


        endNode.GetComponent<Renderer>().material.color = new Color(0.75f, 0.25f, 0.25f);
        endNode.GetComponent<NodeScript>().unbuildable = true;

        GameObject endNodeCol = Instantiate(new GameObject(), endNode.transform);
        endNodeCol.tag = "endNode";
        endNodeCol.layer = 2;
        SphereCollider sc = endNodeCol.AddComponent(typeof(SphereCollider)) as SphereCollider;
        sc.isTrigger = true;
        sc.center = new Vector3(0, 1, 0);

    }

    private void Update()
    {
        if (countDown <= 0f)
        {
            int rnd = Random.Range(0, enemies.Length);
            enemy = enemies[rnd];
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            level++;
        }

        countDown -= Time.deltaTime;
        waveCountdownText.text = "Next wave spawns in:" + " " + Mathf.Floor(countDown).ToString();
        levelCount.text = "Current level:" + " " + level;
    }




    IEnumerator SpawnWave()
    {
        float speed = Random.Range(1, 2);
        waveIndex++;
        if (waveIndex == 5)
        {
            int rnd = Random.Range(0, enemies.Length);
            boss = enemies[rnd];
            waveIndex = -1;
            boss.transform.localScale *= 5;
            SpawnEnemy(boss, bossHP, true, speed);
            boss.transform.localScale /= 5;
            bossHP += 1000;
        }

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(enemy, enemyHP, false, speed);
            yield return new WaitForSeconds(0.5f);
        }

        enemyHP *= 1.03f;


    }

    void SpawnEnemy(GameObject spawn, float hp, bool boss, float speed)
    {
        lastEnemy = Instantiate(spawn, spawnNode.transform.position + offset, spawnNode.transform.rotation);
        lastEnemy.GetComponent<NavMeshAgent>().SetDestination(endNode.transform.position);
        StartCoroutine(checkPath());
        lastEnemy.GetComponent<AIScript>().setHp((int) hp);
        lastEnemy.GetComponent<AIScript>().isBoss = boss;
        lastEnemy.GetComponent<AIScript>().setSpeed(speed);
    }

    IEnumerator checkPath()
    {
        yield return new WaitForSeconds(0.5f);
        if (lastEnemy != null)
        {
            if (lastEnemy.GetComponent<NavMeshAgent>().path.status != NavMeshPathStatus.PathComplete)
            {
                GameObject towerToDestroy = GameObject.FindGameObjectWithTag("Tower");
                towerToDestroy.GetComponent<TowerScript>().node.GetComponent<NodeScript>().hasTower = false;
                Destroy(towerToDestroy);
            }
        }
    }


    public GameObject getLastEnemy()
    {
        return lastEnemy;
    }

}
