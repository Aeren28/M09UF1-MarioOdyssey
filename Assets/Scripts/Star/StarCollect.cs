using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollect : MonoBehaviour
{
    private Audio_Manager audioManager;

    [SerializeField]
    private AudioClip pickStar;

    [SerializeField]
    private UiYouWin youWin;

    private void Start()
    {

        audioManager = Audio_Manager._AUDIO_MANAGER;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            audioManager.ReproduceSound(pickStar);

            Destroy(gameObject);

            youWin.ShowWin();
            Debug.Log("YOU WIN");

        }
    }
}
