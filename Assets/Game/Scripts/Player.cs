using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameData gameData;

    public GameObject woodBack;

    public void AddWoodCount()
    {
        gameData.playerWoodCount += 5;
        woodBack.SetActive(true);
    }
}
