using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class InvisibleSlime : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public SphereCollider sphereCollider;
    public GameObject model;
    public Material inviMaterial;
    public GameObject pointLight;

    public int waypointIndex;
    public Transform[] waypoints = { };

    public bool isHidden = true;
    public AudioSource showAudio;
    public AudioSource hitAudio;
    public int hp = 3;

    private int WalkTriggerHash;
    private bool isCoroutineStarted = false;
    public Mb.FadePanelController fpController;

    private void Awake()
    {
        HideSlime();

        WalkTriggerHash = Animator.StringToHash("WalkTrigger");

        agent.ResetPath();
        agent.SetDestination(waypoints[waypointIndex].position);
        animator.SetTrigger(WalkTriggerHash);

        fpController = FindObjectOfType<Mb.FadePanelController>();
    }

    private void FixedUpdate()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1.0f)
        {
            if (!isCoroutineStarted)
            {
                isCoroutineStarted = true;
                StartCoroutine(Mb.Utils.Wait(Mb.Utils.RandomRange(2.0f, 4.0f), ChangeWaypoint));
            }
        }
    }

    private void ChangeWaypoint()
    {
        isCoroutineStarted = false;

        waypointIndex++;
        if (waypointIndex > waypoints.Length - 1)
        {
            waypointIndex = 0;
        }

        agent.ResetPath();
        agent.SetDestination(waypoints[waypointIndex].position);
        animator.SetTrigger(WalkTriggerHash);
    }

    public void ShowSlime(bool playSound)
    {
        if (!showAudio.isPlaying && playSound)
        {
            showAudio.Play();
        }

        sphereCollider.enabled = true;
        Color c = inviMaterial.color;
        c.a = 255f;
        inviMaterial.color = c;

        pointLight.SetActive(true);

        isHidden = false;
    }

    public void HideSlime()
    {
        sphereCollider.enabled = true;
        Color c = inviMaterial.color;
        c.a = 0;
        inviMaterial.color = c;

        pointLight.SetActive(false);

        isHidden = true;
    }

    public void TakeDamage()
    {
        hitAudio.Play();

        --hp;
        if (hp <= 0)
        {
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
            transform.DOShakeRotation(0.25f, 50f);
        }
    }

    private void OnDeads()
    {
        fpController.OnFadeComplete += OnFadeInComplete;
        fpController.FadeIn();
    }

    void OnFadeInComplete()
    {
        fpController.OnFadeComplete -= OnFadeInComplete;
        SceneManager.LoadScene((int)SceneIndices.COMPLETE);
    }
}
