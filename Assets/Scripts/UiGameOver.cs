using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGameOver : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas; 

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }
}
