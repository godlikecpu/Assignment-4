﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    GameObject spawnNode;
    GameObject endNode;
    public GameObject enemy;
    GameObject lastEnemy;
    Vector3 offset = new Vector3(0, 0.4f, 0);
    NavMeshPath path;

    [Header("Spawn Stuff")]
    private float countDown = 2f;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    private int waveIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        int rnd = Random.Range(0, 16);
        int rndend = Random.Range(48, 63);
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Nodes");
        spawnNode = nodes[rnd];
        spawnNode.GetComponent<Renderer>().material.color = Color.grey;
        spawnNode.GetComponent<NodeScript>().unbuildable = true;
        endNode = nodes[rndend];
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
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Floor(countDown).ToString();
    }




    IEnumerator SpawnWave()
    {
        Debug.Log("Wave incomming!");
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }


    }

    void SpawnEnemy()
    {
        lastEnemy = Instantiate(enemy, spawnNode.transform.position+ offset, spawnNode.transform.rotation);
        lastEnemy.GetComponent<NavMeshAgent>().SetDestination(endNode.transform.position);

    }

    public bool checkPath()
    {
        NavMesh.CalculatePath(spawnNode.transform.position, endNode.transform.position, NavMesh.AllAreas, path);
        Debug.Log("Checked path");
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public GameObject getLastEnemy()
    {
        return lastEnemy;
    }

    // Update is called once per frame

}
