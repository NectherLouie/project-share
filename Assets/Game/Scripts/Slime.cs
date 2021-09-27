using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Slime : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public SphereCollider sphereCollider;
    public GameObject model;
    public GameObject canvasText;

    public int waypointIndex;
    public Transform[] waypoints = { };

    private int WalkTriggerHash;
    private bool isCoroutineStarted = false;

    private void Awake()
    {
        WalkTriggerHash = Animator.StringToHash("WalkTrigger");

        agent.ResetPath();
        agent.SetDestination(waypoints[waypointIndex].position);
        animator.SetTrigger(WalkTriggerHash);

        canvasText.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1.0f)
        {
            if (!isCoroutineStarted)
            {
                isCoroutineStarted = true;
                StartCoroutine(Mb.Utils.Wait(Mb.Utils.RandomRange(8.0f, 12.0f), ChangeWaypoint));
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            canvasText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canvasText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canvasText.SetActive(false);
        }
    }
}
