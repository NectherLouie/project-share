using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanelController : MonoBehaviour
{
    public bool normalAttack;
    public Mb.FadePanelController fpController;

    private void Awake()
    {
        fpController = FindObjectOfType<Mb.FadePanelController>();
    }

    private void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        normalAttack = Input.GetButtonDown("Fire1");

        if (normalAttack)
        {
            fpController.OnFadeComplete += OnFadeInComplete;
            fpController.FadeIn();
        }
    }

    void OnFadeInComplete()
    {
        fpController.OnFadeComplete -= OnFadeInComplete;
        SceneManager.LoadScene((int)SceneIndices.PLAY);
    }

    public void FadeIn()
    {
        fpController.FadeOut();
    }
}
