using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : MonoBehaviour
{
    public AudioSource collect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (!player.hasLampInBack)
            {
                //collect.Play();
                player.AddWoodCount();
                Destroy(gameObject);
            }
        }
    }
}
