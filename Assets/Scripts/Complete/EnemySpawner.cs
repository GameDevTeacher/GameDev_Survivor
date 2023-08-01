using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public float spawnTime;
    private float spawnTimeCounter;

    public float randomX;
    public float randomY;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.time > spawnTimeCounter)
        {
            Instantiate(enemy, SpawnPosition(), quaternion.identity);
            spawnTimeCounter = Time.time + spawnTime;
        }
    }

    private Vector2 SpawnPosition()
    {
        // randomY = Random.Range(1.1f, -1.1f);
         float width = cam.orthographicSize * cam.aspect + 1;
         float height = cam.orthographicSize + 1; // now they spawn just outside

         //randomX = Random.Range(1.1f, -1.1f);
        
        if (randomX > 1f || randomX < -1f)
        {
            var random = Random.Range(0, 1);
            randomY = random == 0 ? 1.1f : -1.1f;
        }
        
        //return cam.ViewportToWorldPoint(new Vector3(randomX, randomY, 0.0f));

        return new Vector2(cam.transform.position.x + Random.Range(-width, width), cam.transform.position.y + Random.Range(-height, height));
    }
}