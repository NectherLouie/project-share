using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePanelController : MonoBehaviour
{
    public bool normalAttack;

    // Update is called once per frame
    void Update()
    {
        normalAttack = Input.GetButtonDown("Fire1");

        if (normalAttack)
        {
            SceneManager.LoadScene(1);
        }
    }
}
