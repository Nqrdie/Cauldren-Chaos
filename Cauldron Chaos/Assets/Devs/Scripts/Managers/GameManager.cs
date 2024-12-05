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
    [SerializeField] private GameObject mixer;
    private bool rotate;
    private bool isRunning = false;

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

        if (rotate)
        {
            mixer.SetActive(true);
            mixer.GetComponent<PlatformRotate>().speed = 8f;
            platform.GetComponent<PlatformRotate>().speed = 3f;
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
                rotate = true;
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
            Instantiate(objects[i], Random.insideUnitSphere + spawnArea.transform.position, objects[i].transform.rotation);
            yield return new WaitForSeconds(objectDelay);
        }
    }

    public IEnumerator Sink()
    {
        Vector3 targetPos = platform.transform.position;
        targetPos.y -= 0.04f;

        while (Mathf.Abs(platform.transform.position.y - targetPos.y) > 0.001f)
        {
            isRunning = true;
            platform.transform.position = Vector3.Lerp(platform.transform.position, targetPos, 0.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        platform.transform.position = targetPos;
        isRunning = false;
    }
}
