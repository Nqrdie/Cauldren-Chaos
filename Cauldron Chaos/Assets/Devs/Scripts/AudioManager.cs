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

     public void PlaySound(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(audioSourceObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip;

        audioSource.volume = volume;  

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource, clipLength);
    }

    public void PlayRandomSound(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, clip.Length);

        AudioSource audioSource = Instantiate(audioSourceObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip[rand];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource, clipLength);
    }
}
