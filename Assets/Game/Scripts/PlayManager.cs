using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameData gameData;

    private void Awake()
    {
        gameData.Reset();
    }
}
