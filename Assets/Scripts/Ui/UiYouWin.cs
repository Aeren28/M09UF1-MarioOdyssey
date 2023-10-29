using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiYouWin : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public void ShowWin()
    {
        canvas.enabled = true;
    }

    public void HideWin()
    {
        canvas.enabled = false;
    }
}
