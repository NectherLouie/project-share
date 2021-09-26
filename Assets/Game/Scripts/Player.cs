using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameData gameData;

    public Campfire campfire;
    public CraftingDesk craftingDesk;

    public GameObject woodBack;
    public GameObject lampBack;
    public GameObject resourceCount;

    public bool canReplenishCampfire;
    public bool replenishCampfire;

    public bool canBuildLamp;
    public bool buildLamp;
    public bool placeLamp;
    public GameObject lanternPrefab;

    public bool hasWoodInBack;
    public bool hasLampInBack;

    public AudioSource replenishAudio;
    public AudioSource buildAudio;
    public AudioSource placeAudio;

    private void Start()
    {
        woodBack.SetActive(false);
        resourceCount.SetActive(false);
    }

    private void Update()
    {
        if (canReplenishCampfire)
        {
            replenishCampfire = Input.GetButtonDown("Jump");

            if (replenishCampfire && gameData.playerWoodCount > 0 && !hasLampInBack)
            {
                Debug.Log("Replenish Campfire");
                replenishAudio.Play();

                if (gameData.fireStrength <= 0) // just once
                {
                    campfire.EnableAllLamps();
                }

                gameData.ReplenishCampfire();

                if (gameData.playerWoodCount <= 0)
                {
                    resourceCount.SetActive(false);
                    woodBack.SetActive(false);

                    hasWoodInBack = false;
                }

                UpdateResourceCount(gameData.playerWoodCount);
            }
        }

        if (canBuildLamp)
        {
            buildLamp = Input.GetButtonDown("Jump");
            if (buildLamp && gameData.playerWoodCount > 0)
            {
                Debug.Log("Build Lamp");
                buildAudio.Play();

                gameData.BuildLamp();

                if (gameData.playerWoodCount <= 0)
                {
                    resourceCount.SetActive(false);
                    woodBack.SetActive(false);

                    hasWoodInBack = false;
                }

                UpdateResourceCount(gameData.playerWoodCount);

                craftingDesk.BuildLamp();
            }
        }
    
        if (gameData.playerLampCount > 0 && gameData.fireStrength > 0)
        {
            placeLamp = Input.GetButtonDown("Fire2");
            if (placeLamp)
            {
                Debug.Log("Place Lamp");
                placeAudio.Play();

                Instantiate(lanternPrefab, transform.position, Quaternion.identity);
                
                gameData.playerLampCount -= 1;

                if (gameData.playerLampCount <= 0)
                {
                    resourceCount.SetActive(false);
                    lampBack.SetActive(false);

                    hasLampInBack = false;
                }

                UpdateResourceCount(gameData.playerLampCount);
            }
        }
    }

    private void UpdateResourceCount(int resourceToCount)
    {
        TMP_Text countText = resourceCount.GetComponent<TMP_Text>();
        countText.text = resourceToCount.ToString();
    }

    public void AddWoodCount()
    {
        if (!hasLampInBack)
        {
            gameData.playerWoodCount += gameData.playAddWoodCount;

            resourceCount.SetActive(true);
            UpdateResourceCount(gameData.playerWoodCount);

            woodBack.SetActive(true);

            hasWoodInBack = true;
        }
    }

    public void AddLampCount()
    {
        gameData.playerLampCount += 1;
        resourceCount.SetActive(true);
        UpdateResourceCount(gameData.playerLampCount);

        lampBack.SetActive(true);

        hasLampInBack = true;
    }

    public void EnableCanReplenishFire()
    {
        canReplenishCampfire = true;
    }

    public void DisableCanReplenishFire()
    {
        canReplenishCampfire = false;
    }

    public void EnableCanBuildLamp()
    {
        canBuildLamp = true;
    }

    public void DisableCanBuildLamp()
    {
        canBuildLamp = false;
    }
}
