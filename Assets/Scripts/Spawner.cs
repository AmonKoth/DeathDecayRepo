using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float spawnTime = 5.0f;
    
    private GameObject spawnParent;
    private bool isSpawning = false;
    private Vector3 spawnPositon;

    public Creep mobToSpawn;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnParent = GameObject.FindGameObjectWithTag("parent");
        spawnPositon = new Vector3(this.transform.position.x+ Random.Range(3.0f, 7.0f),
                                   this.transform.position.y,
                                   this.transform.position.z + Random.Range(3.0f,7.0f));
    }


    IEnumerator SpawnCreep()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnTime);
        Instantiate(mobToSpawn, spawnPositon, Quaternion.identity,spawnParent.transform);
        isSpawning = false;
    }


    // Update is called once per frame
    void Update()
    { 
        if(!isSpawning)
        {
            StartCoroutine("SpawnCreep");
        }    
    }
}
