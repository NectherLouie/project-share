using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CraftingDesk : MonoBehaviour
{
    public GameObject interactionButton;
    public GameData gameData;

    public GameObject lanternResourcePrefab;
    public GameObject costText;

    private void Awake()
    {
        costText.SetActive(false);
    }

    public void BuildLamp()
    {
        Vector3 pos = costText.transform.localPosition;
        pos.y = 1f;
        costText.transform.localPosition = pos;

        costText.transform.DOLocalMoveY(3f, 0.5f).
            OnStart(() =>
            {
                costText.SetActive(true);
            }).
            OnComplete(() =>
            {
                costText.SetActive(false);
            });

        GameObject g = Instantiate(lanternResourcePrefab, transform.position, Quaternion.identity);
        g.transform.localScale = Vector3.zero;

        g.GetComponent<SphereCollider>().enabled = false;

        Vector2 randUnitCircle = Random.insideUnitCircle * 4f;
        Vector3 randPos = new Vector3(transform.position.x + randUnitCircle.x, transform.position.y, transform.position.z + randUnitCircle.y);
        g.transform.DOMove(randPos, 0.5f);
        g.transform.DOScale(Vector3.one, 0.5f)
            .OnComplete(() =>
            {
                g.GetComponent<SphereCollider>().enabled = true;
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.EnableCanBuildLamp();
            interactionButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.DisableCanBuildLamp();
            interactionButton.SetActive(false);
        }
    }
}
