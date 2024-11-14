using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{

    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject[] objects;

    void Start()
    {
        StartCoroutine(spawn());
    }


    IEnumerator SpawnObject()
    {
        int i = Random.Range(0, objects.Length);
        Instantiate(objects[i], Random.insideUnitSphere * 15 + spawnArea.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator spawn()
    {
        for(int i = 0; i < 50; i++)
        {
            StartCoroutine(SpawnObject());
            yield return new WaitForSeconds(2f);
        }
    }
}
