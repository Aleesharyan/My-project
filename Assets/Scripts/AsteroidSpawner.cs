using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    //prefab reference
    public Asteroid asteroidPrefab;

    public float trajectoryVariance = 15.0f;

    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;

    //lets us spawn multiple asteroids at a time
    public int spawnAmount = 1;


    private void Start()
    {
        //spawm will be called repeatingly every 2 seconds
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            //starting with spawn point
            //".normalized" will make the asteroids spawn at the edges of the circle
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);

            //randomises size of asteroid
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

}
