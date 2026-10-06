using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class MastodonController : MonoBehaviour
{
   
    public float pathSpeed = 2.5f;
    private int currentPointIndex = -1;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject closestObstacle;
    private Rigidbody _rb;
    public bool isRidden = false;
    public InputActionReference mastodonAttack;
    [SerializeField] private GameObject breakEffectPrefab;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject VisualClue;
    private void OnEnable()
    {
        mastodonAttack.action.started += OnPrimaryButtonPressed;
        mastodonAttack.action.canceled += OnPrimaryButtonReleased;
    }

    private void OnDestroy()
    {
        mastodonAttack.action.started -= OnPrimaryButtonPressed;
        mastodonAttack.action.canceled -= OnPrimaryButtonReleased;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    void Update()
    {
        if (!isRidden)
        {
            Idle();
        }
        else
        {
            FollowPath();
        }
 
    }

    public void Mount()
    {
        circle.SetActive(false);
        isRidden = true;
        agent.isStopped = false;

        Debug.Log("Mastodon Mounted. Agent Stopped: " + agent.isStopped);
    }

    public void Dismount()
    {
        isRidden = false;
        agent.isStopped = true;

        Debug.Log("Mastodon Dismounted. Agent Stopped: " + agent.isStopped);
    }


    private void Idle()
    {
        agent.ResetPath();
        _rb.isKinematic = true;
        agent.isStopped = true;
        animator.SetBool("isWalking", false); 
    }
    
    
    private void FollowPath()
    {
        _rb.isKinematic = false;
        if (currentPointIndex == -1) 
        {
            currentPointIndex = 0;
            transform.position += Vector3.down;
            agent.SetDestination(WaypointHandler.Instance.Waypoints[currentPointIndex].transform.position);
        }

        animator.SetBool("isWalking", true);
        VisualClue.SetActive(false);

        //current waypoint
        if (agent.remainingDistance < 1.5f && !agent.pathPending)
        {     
            Debug.Log("Has path:" + agent.hasPath);
            if (currentPointIndex >= WaypointHandler.Instance.Waypoints.Count - 1)
            {
                // stop at the final 
                agent.isStopped = true;
                animator.SetBool("isWalking", false); 
                isRidden = false; // for idle animation
                return;
            }
            // next waypoint
            currentPointIndex++;
            agent.SetDestination(WaypointHandler.Instance.Waypoints[currentPointIndex].transform.position);
            Debug.Log(WaypointHandler.Instance.Waypoints[currentPointIndex].name);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the player is close
        if (other.CompareTag("Obstacle"))
        {
            closestObstacle = other.gameObject; // ref to the obstacle
        }
        if (isRidden && closestObstacle == null)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If player leaves the obstacles area, reset the closestObstacle
        
        if (other.CompareTag("Obstacle"))
        {
            closestObstacle = null;
        }
    }

    
    [ContextMenu("destory")]
    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        if (isRidden)
        {
            Debug.Log("Primary button x pressedx");
            animator.SetTrigger("Break");

            // If we're close to an obstacle, break it
            if (closestObstacle != null)
            {
                closestObstacle.SetActive(false);
                if (breakEffectPrefab != null)
                {
                    Instantiate(breakEffectPrefab, closestObstacle.transform.position, Quaternion.identity); // for breaking effect to play
                } 

                Debug.Log("Obstacle broken");
                agent.isStopped = false;
                
            }
        }
    }

    private void OnPrimaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Primary button x released");
        animator.ResetTrigger("Break");

    }
}


