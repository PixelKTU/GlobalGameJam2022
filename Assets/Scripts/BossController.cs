using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static GameEnums;

public class BossController : MonoBehaviour
{
    public BossStates bossState = BossStates.idle;
    
    public float _seeDistance = 10f;
    
    public float _timeBetweenAttacks = 1f;
    
    public float _attackDistance = 2f;
    
    private Transform _target;

    private NavMeshAgent _agent;

    private Animator _animator;

    public int _currentMemoryWaypoint = 0;
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private GameObject playerCamera;

    public void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        BossStates bossState = BossStates.idle;
        
        _animator = GetComponent<Animator>();
        
        _animator.SetBool("idle", true);
        _animator.SetBool("walk", false);
    }

    private float _timeSinceLastAttack = 0;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerMemory();
        }
        
        if (bossState == BossStates.idle)
        {
            RaycastHit hit;
            Vector3 direction = _target.position - transform.position;

            if (CheckRange(_seeDistance))
            {
                bossState = BossStates.chase;
                _agent.SetDestination(_target.position);
                    
                _animator.SetBool("walk", true);
                _animator.SetBool("idle", false);
            }
        }

        _timeSinceLastAttack += Time.deltaTime;
        
        if (bossState == BossStates.chase)
        {
            if (CheckRange(_seeDistance))
            {
                if(CheckRange(_attackDistance))
                {
                    _agent.isStopped = true;
                    bossState = BossStates.attack;
                    _animator.SetTrigger("attack");
                }
                else
                {
                    _animator.SetBool("walk", true);
                    _animator.SetBool("idle", false);
                    _agent.SetDestination(_target.position);
                }

            }
            else
            {
                _animator.SetBool("walk", true);
                _animator.SetBool("idle", false);
                _agent.SetDestination(waypoints[_currentMemoryWaypoint].position);
            }
            return;
        }
        
        if (bossState == BossStates.attack)
        {
            if(CheckRange(_attackDistance))
            {
                if (_timeSinceLastAttack >= _timeBetweenAttacks)
                {
                    _animator.SetTrigger("attack");
                    _animator.SetBool("walk", false);
                    _timeSinceLastAttack = 0;

                    Invoke(nameof(EndAttack), 1f);
                }
            }
            else
            {
                bossState = BossStates.chase;
                _agent.isStopped = false;

                _agent.SetDestination(_target.position);
                _animator.SetBool("walk", true);
                _animator.SetBool("idle", false);
            }
        }
    }

    private bool CheckRange(float targetDistance)
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        
        if (distance <= targetDistance)
            return true;
        
        return false;
    }

    public void TriggerMemory()
    {
        bossState = BossStates.memory;
        _currentMemoryWaypoint++;
        _animator.SetTrigger("memory");
        
        _agent.isStopped = true;
        
        _animator.SetBool("walk", false);
        _animator.SetBool("idle", false);
        
        playerCamera.SetActive(false);
        
        Invoke(nameof(EndMemory), 5.5f);
    }

    private void EndMemory()
    {
        bossState = BossStates.idle;
        
        _agent.isStopped = false;
        
        _animator.SetBool("walk", false);
        _animator.SetBool("idle", true);
        
        playerCamera.SetActive(true);
        
        _agent.SetDestination(waypoints[_currentMemoryWaypoint].position);
    }

    private void EndAttack()
    {
        if(CheckRange(_attackDistance - 0.5f)) GameState.Instance.Story.WakeUp();
    }
}
