using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager _AUDIO_MANAGER;

    private AudioSource audioSource;

    private void Awake()
    {
        if(_AUDIO_MANAGER == null)
        {
            _AUDIO_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ReproduceSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
