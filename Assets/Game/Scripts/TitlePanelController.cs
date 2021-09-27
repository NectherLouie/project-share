using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitlePanelController : MonoBehaviour
{
    public static bool isTitleActive = true;

    public bool normalAttack;

    public Camera mainCam;
    public Vector3 beforePos;
    public float beforeRot;

    public Vector3 afterPos;
    public float afterRot;

    public Action OnFadeComplete;
    public float fadeDuration = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (!isTitleActive)
        {
            return;
        }

        normalAttack = Input.GetButtonDown("Fire1");

        if (normalAttack)
        {
            mainCam.transform.localPosition = beforePos;

            Quaternion rotation = mainCam.transform.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.x = beforeRot;
            rotation.eulerAngles = eulerAngles;
            mainCam.transform.rotation = rotation;

            // animate
            mainCam.transform.DOLocalMove(afterPos, fadeDuration + 0.2f);

            Vector3 afterEuler = rotation.eulerAngles;
            afterEuler.x = afterRot;
            mainCam.transform.DORotate(afterEuler, fadeDuration + 0.2f);

            OnFadeComplete += OnFadeOutComplete;
            FadeOut();
        }
    }

    private void OnFadeOutComplete()
    {
        OnFadeComplete -= OnFadeOutComplete;
        isTitleActive = false;
    }

    public void FadeIn()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();

        cg.alpha = 0;
        cg.DOFade(1.0f, fadeDuration)
            .OnComplete(() => OnFadeComplete?.Invoke());
    }

    public void FadeOut()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();

        cg.alpha = 1.0f;
        cg.DOFade(0, fadeDuration)
            .OnComplete(() => OnFadeComplete?.Invoke());
    }
}
