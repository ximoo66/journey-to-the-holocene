using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowMastodon : MonoBehaviour
{
    private NavMeshAgent ai;
    [SerializeField] private GameObject mainMastodon;
    [SerializeField] private NavMeshAgent aiMain;
    [SerializeField] private MastodonController _mastodonController;
    private Animator animator;
    Vector3 dest;
    private bool isWalking;

    private void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", false); 
        
    }

    void Update()
    {

        
        if (!_mastodonController.isRidden || aiMain.isStopped) 
        {
            StopFollowing();
        }
        else
        {
            FollowMaster();
        }

        if (Vector3.Distance(transform.position, mainMastodon.transform.position) < 3f)
        {
            StopFollowing();
        }
    }

    private void FollowMaster()
    {
        ai.isStopped = false;
        dest = mainMastodon.transform.position;
        ai.destination = dest;
        animator.SetBool("isWalking", true);
        
    }

    private void StopFollowing()
    {
        ai.isStopped = true;
        animator.SetBool("isWalking", false); 
    }
      
}