using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternResource : MonoBehaviour
{
    public AudioSource collect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (!player.hasWoodInBack)
            {
                collect.Play();
                player.AddLampCount();
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
