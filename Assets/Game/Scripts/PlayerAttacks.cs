using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public PlayerAttackArea playerAttackArea;

    public void StartAttack()
    {
        StartCoroutine(Mb.Utils.Wait(0.2f, OnStartAttackComplete));
    }

    private void OnStartAttackComplete()
    {
        playerAttackArea.gameObject.SetActive(true);
        StartCoroutine(Mb.Utils.Wait(0.1f, OnMidAttackComplete));
    }

    private void OnMidAttackComplete()
    {
        playerAttackArea.gameObject.SetActive(false);
    }
}
