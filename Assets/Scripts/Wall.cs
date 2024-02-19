using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject[] obstacles;
    private void Start()
    {
        int l = Random.Range(1, obstacles.Length - 1);
        for (int i=0;i<l;i++)
        {
            obstacles[Random.Range(0, obstacles.Length - 1)].SetActive(true);
        }
    }
}
