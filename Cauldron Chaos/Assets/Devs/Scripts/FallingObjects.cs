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


    private void SpawnObject()
    {
        int i = Random.Range(0, objects.Length);
        Instantiate(objects[i], Random.insideUnitSphere * 15 + spawnArea.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator spawn()
    {
        for(int i = 0; i < 50; i++)
        {
            SpawnObject();
            yield return new WaitForSeconds(2f);
        }
    }
}
