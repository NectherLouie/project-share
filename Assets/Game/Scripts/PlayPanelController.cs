using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPanelController : MonoBehaviour
{
    public Mb.FadePanelController fpController;

    private void Awake()
    {
        fpController = FindObjectOfType<Mb.FadePanelController>();
    }

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        fpController.FadeOut();
    }
}
