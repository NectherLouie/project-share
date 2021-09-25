using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeController : MonoBehaviour
{
    public List<GameObject> treeModels = new List<GameObject>();

    public GameObject woodResourcePrefab;
    public int hp = 3;

    private void Awake()
    {
        GetComponent<CapsuleCollider>().enabled = true;

        Quaternion rotation = transform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.y = Mb.Utils.RandomRange(0, 360f);
        rotation.eulerAngles = eulerAngles;
        transform.rotation = rotation;

        foreach (GameObject g in treeModels)
        {
            g.SetActive(false);
        }

        int index = Mb.Utils.RandomRange(0, treeModels.Count - 1);
        treeModels[index].SetActive(true);
    }

    public void TakeDamage()
    {
        --hp;
        if (hp <= 0)
        {
            GetComponent<CapsuleCollider>().enabled = false;

            // Rewind to setup
            Quaternion rotation = transform.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.z = 0f;
            rotation.eulerAngles = eulerAngles;
            transform.rotation = rotation;

            // animate
            Vector3 rot = transform.rotation.eulerAngles;
            
            transform.DORotate(new Vector3(rot.x, rot.y, 120f), 0.75f)
                .SetEase(Ease.InBack)
                .OnComplete(OnDeads);
        }
        else
        {
            transform.DOShakeRotation(0.25f, 5f);
        }
    }

    private void OnDeads()
    {
        Vector3 pos = transform.position;
        Instantiate(woodResourcePrefab, new Vector3(pos.x, pos.y + 1f, pos.z), transform.rotation);
        Destroy(gameObject, 1f);
    }

}
