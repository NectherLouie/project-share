using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.AddWoodCount();
            Destroy(gameObject, 0.01f);
        }
    }
}
