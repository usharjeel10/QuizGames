using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

    public GameObject pipe;
    public float maxYPos;
    public float spawnTime; // interval used to spawn pipes

	// Use this for initialization
	void Start () {
       // StartSpawningPipes();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSpawningPipes()
    {
        InvokeRepeating("SpawnPipe", 0.2f, spawnTime);
    }

    public void StopSpawningPipes()
    {
        CancelInvoke("SpawnPipe");
    }

    void SpawnPipe()
    {
        Instantiate(pipe, new Vector2(transform.position.x, Random.Range(-maxYPos, maxYPos)), Quaternion.identity);
    }
}
