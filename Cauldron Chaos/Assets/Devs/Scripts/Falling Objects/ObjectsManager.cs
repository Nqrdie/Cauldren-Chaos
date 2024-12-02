using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{

    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject[] objects;

    public void SpawnObject()
    {
        int i = Random.Range(0, objects.Length);
        Instantiate(objects[i], Random.insideUnitSphere + spawnArea.transform.position, Quaternion.identity);
    }
}
