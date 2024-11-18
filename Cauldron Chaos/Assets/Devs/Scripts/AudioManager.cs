using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSourceObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
    }

     public void PlaySound(AudioClip clip, float volume = 1f, Vector3 spawnPos = default(Vector3))
    {
        AudioSource audioSource = Instantiate(audioSourceObject, spawnPos, Quaternion.identity);

        audioSource.clip = clip;

        audioSource.volume = volume;  

        audioSource.Play();

        float clipLength = audioSource.clip.length;

       // Destroy(audioSource, clipLength);
    }

    public void PlayRandomSound(AudioClip[] clip, Vector3 spawnPos = default(Vector3), float volume = default(float))
    {
        int rand = Random.Range(0, clip.Length);

        AudioSource audioSource = Instantiate(audioSourceObject, spawnPos, Quaternion.identity);

        audioSource.clip = clip[rand];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource, clipLength);
    }
}
