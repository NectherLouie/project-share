using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public GameObject pointLight;
    public SphereCollider sphereCollider;

    private void Awake()
    {
        EnableLight();
    }

    public void EnableLight()
    {
        sphereCollider.enabled = true;
        pointLight.SetActive(true);
    }

    public void DisableLight()
    {
        sphereCollider.enabled = false;
        pointLight.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InvisibleSlime slime))
        {
            Debug.Log("Lantern Slime Collided");
            slime.ShowSlime(true);
        }

        if (other.TryGetComponent(out Player player))
        {
            player.EnableCanPickup(gameObject.GetInstanceID() + "_", this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out InvisibleSlime slime))
        {
            Debug.Log("Lantern Slime Stay");
            slime.ShowSlime(false);
        }

        if (other.TryGetComponent(out Player player))
        {
            player.EnableCanPickup(gameObject.GetInstanceID() + "_", this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InvisibleSlime slime))
        {
            Debug.Log("Hide Slime");
            slime.HideSlime();
        }

        if (other.TryGetComponent(out Player player))
        {
            player.DisableCanPickup(gameObject.GetInstanceID() + "_");
        }
    }
}
