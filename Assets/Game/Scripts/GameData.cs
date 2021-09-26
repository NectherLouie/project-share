using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    public int playerWoodCount = 0;
    public int playAddWoodCount = 5;

    public float fireStrength = 300;
    public float fireCost = 5;
    public float fireMultiplier = 4;

    public int playerLampCount = 0;
    public int lampCost = 5;

    public void Reset()
    {
        playerWoodCount = 0;
        playAddWoodCount = 5;

        fireStrength = 300;
        fireCost = 5;
        fireMultiplier = 4;

        playerLampCount = 0;
        lampCost = 5;
    }

    public void ReplenishCampfire()
    {
        if (playerWoodCount >= fireCost)
        {
            fireStrength += fireCost * fireMultiplier;
            playerWoodCount -= (int)fireCost;
        }
    }

    public void BuildLamp()
    {
        if (playerWoodCount >= lampCost)
        {
            playerWoodCount -= lampCost;
        }
    }
}
