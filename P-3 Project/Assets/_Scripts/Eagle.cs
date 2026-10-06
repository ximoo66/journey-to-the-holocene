using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Eagle : MonoBehaviour
{
    private Animator _anim;
    private Transform _player;

    // Animator hashes
    private readonly int _isFlyingHash = Animator.StringToHash("IsFlying");
    private readonly int _isGlidingHash = Animator.StringToHash("IsGliding");
    private readonly int _doTakeoffHash = Animator.StringToHash("DoTakeoff");
    private readonly int _doLandingHash = Animator.StringToHash("DoLanding");

    [SerializeField] private float speed = 5f;
    [SerializeField] private float circlingSpeed = 2f;
    [SerializeField] private float circlingRadius = 2.5f;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<float> circlingDurations;
    private int _currentWaypointIndex = 0;

    [SerializeField] private GameObject VisualCue;
    [SerializeField] private Transform finalTriggerArea;

    private bool _isCircling = false;
    private bool _isFollowingPlayer = false;
    private bool _hasLandedFinal = false;
    private Vector3 targetPos;
    private Vector3 _lastPlayerPosition;

    // Eagle position relative to player
    private float followDistanceAhead = 2.5f;
    private float followDistanceRight = 2.5f;
    private float followHeight = 1.5f;

    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("No Animator found on Eagle!!!");

        if (waypoints.Count == 0)
            Debug.LogError("No waypoints assigned!");

        if (circlingDurations.Count != waypoints.Count)
            Debug.LogError("Waypoints and circling durations count mismatch!");

        // Find player dynamically
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _player = playerObject.transform;
            _lastPlayerPosition = _player.position;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        StartFlying();
    }

    void Update()
    {
        if (_isFollowingPlayer && !_hasLandedFinal)
        {
            FollowPlayer();
            return;
        }

        if (_anim.GetBool(_isFlyingHash) && !_isCircling)
        {
            MoveToTarget();
        }

        if (!_isFollowingPlayer && VisualCue.gameObject.activeSelf)
        {
            StartFollowingPlayer();
        }
    }

    private void MoveToTarget()
    {
        var targetRotation = targetPos - transform.position;
        transform.forward = Vector3.Slerp(transform.forward, targetRotation, Time.deltaTime);
        transform.position += transform.forward * (Time.deltaTime * speed);

        if (Time.frameCount % 333 == 0)
            _anim.SetBool(_isGlidingHash, !_anim.GetBool(_isGlidingHash));

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (_currentWaypointIndex == waypoints.Count - 1)
            {
                Land();
                return;
            }
            StartCoroutine(CircleTargetPos(circlingDurations[_currentWaypointIndex]));
        }
    }

    public void StartFlying()
    {
        _anim.SetTrigger(_doTakeoffHash);
        _anim.SetBool(_isFlyingHash, true);
        targetPos = waypoints[_currentWaypointIndex].position + Vector3.up * 4;
    }

    private void Land()
    {
        _anim.SetTrigger(_doLandingHash);
        _anim.SetBool(_isFlyingHash, false);
    }

    private void StartFollowingPlayer()
    {
        _isFollowingPlayer = true;
        _anim.SetTrigger(_doTakeoffHash);
        _anim.SetBool(_isFlyingHash, true);
    }

    private void FollowPlayer()
    {
        if (_player == null) return;

        float distanceToFinalArea = Vector3.Distance(transform.position, finalTriggerArea.position);

        if (distanceToFinalArea < 2.0f)
        {
            _isFollowingPlayer = false;
            _hasLandedFinal = true;
            Land();
            return;
        }

        // Calculate target position slightly **in front**, **to the right**, and **above the player**
        Vector3 forwardOffset = _player.forward * followDistanceAhead;
        Vector3 rightOffset = _player.right * followDistanceRight;
        Vector3 heightOffset = Vector3.up * followHeight;
        Vector3 targetPosition = _player.position + forwardOffset + rightOffset + heightOffset;

        // Smooth movement to target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

        // Rotate eagle to face the same direction as the player
        transform.rotation = Quaternion.Slerp(transform.rotation, _player.rotation, Time.deltaTime * 5f);
    }


    private IEnumerator CircleTargetPos(float duration)
    {
        _isCircling = true;
        float angle = 0f;
        float timer = 0f;

        while (timer < duration)
        {
            angle += circlingSpeed * Time.deltaTime;
            float x = targetPos.x + Mathf.Cos(angle) * circlingRadius;
            float z = targetPos.z + Mathf.Sin(angle) * circlingRadius;
            transform.position = new Vector3(x, transform.position.y, z);

            timer += Time.deltaTime;
            yield return null;
        }

        _isCircling = false;
        AdvanceToNextWaypoint();
    }

    private void AdvanceToNextWaypoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= waypoints.Count) return;

        targetPos = waypoints[_currentWaypointIndex].position + Vector3.up * 4;
    }
}
