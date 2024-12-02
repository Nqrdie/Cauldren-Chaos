using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private float timer = 0f;
    private int intTimer;
    private List<GameObject> players = new List<GameObject>();
    //[SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject playerPrefab;
    private float objectDelay;
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private GameObject platform;
    bool isRunning = false;

    private void Start()
    {
        StartCoroutine(SpawnObject());
        PlayerConfigManager.PlayerConfig[] playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject GO = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            GO.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            players.Add(GO);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime * 1;
        if (timer > 1f)
        {
            intTimer += 1;
            timer = 0f;
        }
        print(intTimer);
        foreach (GameObject player in players)
        {
            if(!player.activeInHierarchy)
            {
                players.Remove(player);
                Destroy(player);
            }
        }

        if (players.Count <= 0)
        {
            //end game
        }

        switch (intTimer) 
        {
            case 0:
                print("lol");
                objectDelay = 2.5f;
                break;
            case 20:
                objectDelay = 2f;
                if(!isRunning)
                    StartCoroutine(Sink());
                print("lol");
                break;
            case 30:
                objectDelay = 1.5f;
                //rotateplatform
                break;
            case 40:
                objectDelay = 1f;
                if (!isRunning)
                    StartCoroutine(Sink());
                break;
            case 50:
                objectDelay = 0.8f;
                if (!isRunning)
                    StartCoroutine(Sink());
                break;
            case 60:
                objectDelay = 0.6f;
                if (!isRunning)
                    StartCoroutine(Sink());
                break;
            case 70:
                objectDelay = 0.5f;
                if (!isRunning)
                    StartCoroutine(Sink());
                break;
            case 80:
                if (!isRunning)
                    StartCoroutine(Sink());
                break;  
        }



    }

    public IEnumerator SpawnObject()
    {
        while (true)
        {
            int i = Random.Range(0, objects.Length);
            Instantiate(objects[i], Random.insideUnitSphere + spawnArea.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(objectDelay);
        }
    }

    public IEnumerator Sink()
    {
        Vector3 pos = platform.transform.position;
        pos.y = platform.transform.position.y - 0.04f;
        while (platform.transform.position.y != pos.y)
        {
            isRunning = true;
            print("lol2");
            platform.transform.position = Vector3.Lerp(platform.transform.position, pos, 1f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isRunning = false;
    }
}
