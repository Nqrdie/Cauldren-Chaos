using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{

    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject[] objects;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }


    IEnumerator SpawnObject()
    {
        int i = Random.Range(0, objects.Length);
        Instantiate(objects[i], Random.insideUnitSphere * 15 + spawnArea.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
    }
}
