using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{   
    public GameObject cubePreFab;
    [SerializeField]private float spawnTime;
    private float timeUntilSpawn;
    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <=0)
        {
            Instantiate(cubePreFab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = spawnTime;
    }
}
