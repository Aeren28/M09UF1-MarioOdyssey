using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private Audio_Manager audioManager;

    private static int getCoins;

    [SerializeField]
    private AudioClip pickCoin;


    private void Start()
    {
        getCoins = 0;

        audioManager = Audio_Manager._AUDIO_MANAGER;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            getCoins++;
            audioManager.ReproduceSound(pickCoin);

            Destroy(gameObject);
        }
    }

}
