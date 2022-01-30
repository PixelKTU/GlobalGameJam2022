using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float _seeDistance = 5f;
    public float _timeBetweenAttacks = 1f;
    public float _timeBetweenStuns = 5f;
    public float _attackDistance = 2f;
    
    private Transform _target;
    private NavMeshAgent _agent;
    private Animator _animator;

    public int _currentMemory = 0;
    [SerializeField] private Transform[] _waypoints;

    private bool isStunned = false;

    public float _hurtTime = 1f;
    public float _memoryTriggerTime = 5.5f;
    
    private float _timeSinceLastAttack = 0;
    private float _timeSinceLastHurt = 0;

    private bool isAttacking = false;
    private void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _currentMemory = 0;

        transform.position = _waypoints[_currentMemory].position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Hurt();
        }

        _timeSinceLastAttack += Time.deltaTime;
        _timeSinceLastHurt += Time.deltaTime;
        
        if (isStunned)
        {
            _agent.isStopped = true;
        }
        else
        {
            if(!isAttacking) _agent.isStopped = false;
            
            if (!CheckRange(_seeDistance))
            {
                var distanceToWaypoint = Vector3.Distance(_waypoints[_currentMemory].position ,transform.position);
                if (distanceToWaypoint > 0.5f)
                {
                    AnimatorWalk();
                    _agent.SetDestination(_waypoints[_currentMemory].position);
                }
                else
                {
                    AnimatorIdle();
                }
            }
            else
            {
                if (CheckRange(_attackDistance))
                {
                    RotateTowardsPlayer();
                    
                    if (_timeSinceLastAttack >= _timeBetweenAttacks)
                    {
                        Attack();
                    }
                }
                else
                {
                    AnimatorWalk();
                    _agent.SetDestination(_target.position);
                }
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
        if (_currentMemory+1 != _waypoints.Length)
        {
            _currentMemory++;
        }

        isStunned = true;
        AnimatorMemory();
        
        StartCoroutine(nameof(EndMemoryTrigger));
    }

    private void Hurt()
    {
        if (_timeSinceLastHurt > _timeBetweenStuns)
        {
            isStunned = true;

            AnimatorHit();
            
            _timeSinceLastHurt = 0;
            
            StartCoroutine(nameof(EndHurt));
        }
    }

    private void Attack()
    {
        AnimatorAttack();
        _timeSinceLastAttack = 0;
        _agent.isStopped = true;
        isAttacking = true;

        StartCoroutine(nameof(EndAttack));
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    IEnumerator EndHurt()
    {
        yield return new WaitForSeconds(_hurtTime);

        isStunned = false;
    }
    
    IEnumerator EndMemoryTrigger()
    {
        yield return new WaitForSeconds(_memoryTriggerTime);

        isStunned = false;
    }
    
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1f);

        isAttacking = false;
        _agent.isStopped = false;
    }

    private void CleanAnimatorBooleans()
    {
        _animator.SetBool("walk", false);
        _animator.SetBool("idle", false);
    }

    private void AnimatorWalk()
    {
        _animator.SetBool("walk", true);
        _animator.SetBool("idle", false);
    }

    private void AnimatorIdle()
    {
        _animator.SetBool("idle", true);
        _animator.SetBool("walk", false);
    }

    private void AnimatorMemory()
    {
        CleanAnimatorBooleans();
        _animator.SetTrigger("memory");
    }
    
    private void AnimatorAttack()
    {
        CleanAnimatorBooleans();
        _animator.SetTrigger("attack");
    }
    
    public void AnimatorHit()
    {
        CleanAnimatorBooleans();
        _animator.SetTrigger("hit");
    }
}
