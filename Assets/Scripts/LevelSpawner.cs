using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    public float spawnDelay;
    public float spawnRange;
    private List<GameObject> spawnedParts=new List<GameObject>();
    private void OnEnable()
    {
        GameManager.onGameStarted += StartCreate;
    }
    private void OnDisable()
    {
        GameManager.onGameStarted -= StartCreate;
    }
    private void StartCreate()
    {
        StartCoroutine(this.SpawnPart());
        Debug.Log("StartCreate");
    }
    private IEnumerator SpawnPart()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (spawnedParts.Count!=0)
        {
            GameObject newPart = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)]);
            
            newPart.transform.position = new Vector3(spawnedParts[0].transform.position.x, spawnedParts[spawnedParts.Count - 1].transform.position.y + spawnRange, spawnedParts[0].transform.position.z);
            spawnedParts.Add(newPart);
        }
        else
        {
            GameObject newPart = Instantiate(levelPrefabs[0]);
            
            newPart.transform.position = new Vector3(1.254f, 10.8f, 0);
            spawnedParts.Add(newPart);

        }
        Debug.Log(GameManager.instance.IsGameStarted());
        if (GameManager.instance.IsGameStarted())
        {
            StartCoroutine(SpawnPart());
        }
    }
}
