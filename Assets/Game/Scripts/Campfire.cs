using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public GameObject interactionButton;
    public GameData gameData;

    public GameObject extraFire;
    public ParticleSystem fireParticle;
    public GameObject pointLight;

    public Lantern[] lamps = { };

    private void Update()
    {
        if (gameData.fireStrength <= 0)
        {
            return;
        }

        if (gameData.fireStrength > 0)
        {
            extraFire.SetActive(true);
            pointLight.SetActive(true);
            fireParticle.gameObject.SetActive(true);

            gameData.fireStrength -= Time.deltaTime * (gameData.fireMultiplier * 0.5f);

            if (gameData.fireStrength <= 0)
            {
                extraFire.SetActive(false);
                fireParticle.gameObject.SetActive(false);
                pointLight.SetActive(false);
                gameData.fireStrength = 0;

                DisableAllLamps();
            }

            ParticleSystem.MainModule main = fireParticle.main;

            if (gameData.fireStrength > 0 && gameData.fireStrength <= 20)
            {
                main.startSize = 0.25f;
            }
            else if (gameData.fireStrength > 20 && gameData.fireStrength <= 100)
            {
                main.startSize = 1f;
            }
            else if (gameData.fireStrength > 100 && gameData.fireStrength <= 200)
            {
                main.startSize = 2f;
            }
            else if (gameData.fireStrength > 200)
            {
                main.startSize = 3f;
            }
        }
        else
        {
            
        }
    }

    public void EnableAllLamps()
    {
        lamps = FindObjectsOfType<Lantern>();

        for (int i = 0; i < lamps.Length; ++i)
        {
            lamps[i].EnableLight();
        }
    }

    public void DisableAllLamps()
    {
        lamps = FindObjectsOfType<Lantern>();

        for (int i = 0; i < lamps.Length; ++i)
        {
            lamps[i].DisableLight();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.EnableCanReplenishFire();
            interactionButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.DisableCanReplenishFire();
            interactionButton.SetActive(false);
        }
    }
}
