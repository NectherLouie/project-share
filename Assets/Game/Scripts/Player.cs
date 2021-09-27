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
    public bool canPickup;
    public bool pickup;
    public List<Lantern> lanternsToPickup = new List<Lantern>();

    public bool hasWoodInBack;
    public bool hasLampInBack;

    public AudioSource replenishAudio;
    public AudioSource buildAudio;
    public AudioSource buildErrorAudio;
    public AudioSource placeAudio;
    public AudioSource pickupAudio;
    public AudioSource lampCollectAudio;
    public AudioSource woodCollectAudio;

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
            bool hasHitMax = (gameData.playerLampCount + gameData.lampCount) >= gameData.lampMaxCount;
            bool canAffordBuild = gameData.playerWoodCount >= gameData.lampCost;
            if (buildLamp && gameData.playerWoodCount > 0 && canAffordBuild && !hasHitMax)
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
            
            if (hasHitMax)
            {
                if (!buildErrorAudio.isPlaying)
                    buildErrorAudio.Play();
            }
        }

        if (canPickup)
        {
            pickup = Input.GetButtonDown("Fire3");
            if (pickup && lanternsToPickup.Count > 0 && !hasWoodInBack)
            {
                Destroy(lanternsToPickup[0].gameObject);
                lanternsToPickup.Clear();
                AddLampCount();
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
                gameData.lampCount += 1;

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
            woodCollectAudio.Play();

            gameData.playerWoodCount += gameData.playAddWoodCount;

            resourceCount.SetActive(true);
            UpdateResourceCount(gameData.playerWoodCount);

            woodBack.SetActive(true);

            hasWoodInBack = true;
        }
    }

    public void AddLampCount()
    {
        lampCollectAudio.Play();

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
        bool hasHitMax = (gameData.playerLampCount + gameData.lampCount) >= gameData.lampMaxCount;

        if (!hasHitMax)
            canBuildLamp = true;
    }

    public void DisableCanBuildLamp()
    {
        canBuildLamp = false;
    }

    public void EnableCanPickup(string key, Lantern value)
    {
        if (lanternsToPickup.Count == 0)
        {
            lanternsToPickup.Add(value);
        }

        canPickup = true;
    }

    public void DisableCanPickup(string key)
    {
        lanternsToPickup.Clear();

        canPickup = false;
    }
}
