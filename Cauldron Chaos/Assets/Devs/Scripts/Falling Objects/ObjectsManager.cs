using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject[] objects;
    

    void Start()
    {
        StartCoroutine(Spawn());
    }

    public void SpawnObject()
    {
        int i = Random.Range(0, objects.Length);
        Instantiate(objects[i], Random.insideUnitSphere + spawnArea.transform.position, Quaternion.identity);
    }
    IEnumerator Spawn()
    {
        for(int i = 0; i < Mathf.Infinity; i++)
        {
            SpawnObject();
            yield return new WaitForSeconds(2f);
        }
    }
}
