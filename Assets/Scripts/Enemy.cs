using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Audio_Manager audioManager;

    [SerializeField]
    private UiGameOver gameOver;

    [SerializeField]
    private AudioClip noAlive;

    private void Start()
    {
        audioManager = Audio_Manager._AUDIO_MANAGER;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Destroy(player);

            audioManager.ReproduceSound(noAlive);
            gameOver.Show();

            Debug.Log("GAME OVER");
        }
    }

}
