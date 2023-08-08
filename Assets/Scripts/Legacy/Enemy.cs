using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Transform _enemyEye;
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _checkRadius = 0.4f;

    int _currentTarget = 0;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;
    public bool isRandomPointActive = false;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        _agent.destination = _targetPoints[_currentTarget].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            Idle();
        }
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                //AttackPlayer
                AttackPlayer();
            }
            else
            {
                //FollowPlayer
                FollowPlayer();
            }
        }
    }

    void Idle()
    {   
        //Choose A random target point and move there if randomPoint Active is true
        if (_agent.remainingDistance < 0.1f)
        {
            if (isRandomPointActive)
            {
                _currentTarget = Random.Range(0, _targetPoints.Length);
            }
            else
            {
                _currentTarget++;
            }
            

            if (_currentTarget >= _targetPoints.Length)
            {
                _currentTarget = 0;
            }

            _agent.destination = _targetPoints[_currentTarget].position;
        }

        //Check for Player
        if (Physics.SphereCast(_enemyEye.position, _checkRadius, transform.forward,out RaycastHit hit, _playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("PlayerFOund");
                isIdle = false;
                isPlayerFound = true;
                player = hit.transform;
                _agent.destination = player.position;
            }
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > 10)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            //Attack
            if (Vector3.Distance(transform.position, player.position) < 2)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            _agent.destination = player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking Player");
        if (Vector3.Distance(transform.position, player.position) > 2)
        {
            isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_enemyEye.position, _checkRadius);
        Gizmos.DrawWireSphere(_enemyEye.position + _enemyEye.forward * _playerCheckDistance, _checkRadius);

        Gizmos.DrawLine(_enemyEye.position, _enemyEye.position + _enemyEye.forward * _playerCheckDistance);
        
    }
    //void FindTarget()
    //{
    //    _agent.SetDestination(_target.position);
    //}
}
