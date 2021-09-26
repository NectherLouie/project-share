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
        if (other.transform.TryGetComponent(out TreeController tree))
        {
            Debug.Log("OnTriggerEnter " + other.transform.name);
            tree.TakeDamage();
        }

        if (other.TryGetComponent(out InvisibleSlime slime))
        {
            if (!slime.isHidden)
            {
                slime.TakeDamage();
            }
        }
    }
}
