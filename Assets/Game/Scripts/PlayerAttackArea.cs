using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " + other.transform.name);
        if (other.transform.TryGetComponent(out TreeController tree))
        {
            tree.TakeDamage();
        }
    }
}
