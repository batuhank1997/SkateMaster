using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.I.isGameStart = true;
        GameManager.I.StartTimeline();
        Destroy(gameObject);
    }
}
