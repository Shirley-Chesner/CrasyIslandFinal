using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, WhatIsPlayer;
    private Animator animator;
    public bool isFightingAnimationPlaying;

    public AudioClip walkingClip;
    public AudioClip fightingClip;
    AudioSource audioSrc;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private RaycastHit slopeHit;

    // Attacking
    public bool alreadyAttacked;
    public float timeBetweenAttacks;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isFightingAnimationPlaying = false;
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = walkingClip;
    }

    private void Update()
    {
        // Check for sign and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!isFightingAnimationPlaying)
        {
            if (!playerInSightRange && !playerInAttackRange) patroling();
            if (playerInSightRange && playerInAttackRange) attackPlayer();
            if (playerInSightRange && !playerInAttackRange) chasePlayer();
        }
    }

    private void patroling()
    {
        animator.SetBool("isFighting", false);
        animator.SetBool("isRunning", false);
        if (!walkPointSet) searchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        // If enemy is blocked search a new walk point
        NavMeshHit hit;
        bool blocked = NavMesh.Raycast(transform.position, walkPoint, out hit, NavMesh.AllAreas);

        if (blocked)
        {
            searchWalkPoint();
        }

        // Fix slope 
        Vector3 m = GetSlopeMovementDirection();
        walkPoint += m;

        // Check if walkpoint reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 2f) walkPointSet = false;
        if (!audioSrc.clip.Equals(walkingClip)) audioSrc.Stop();
        //audioSrc.Play();
    }

    private void searchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private Vector3 GetSlopeMovementDirection()
    {
        return Vector3.ProjectOnPlane(walkPoint, slopeHit.normal).normalized;
    }

    private void chasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isFighting", false);
        animator.SetBool("isRunning", true);
        if (!audioSrc.clip.Equals(walkingClip)) audioSrc.Stop();
        //audioSrc.Play();
    }

    private void attackPlayer()
    {
        // Make sure the enemy doesnt move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetBool("isRunning", false);
        if (!alreadyAttacked)
        {
            // Attack code here
            animator.SetBool("isRunning", false);
            animator.SetBool("isFighting", true);
            alreadyAttacked = true;
        //   Invoke(nameof(resetAttack), timeBetweenAttacks);
        }
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    public void fightAnimationIsPlaying()
    {
        alreadyAttacked = true;
        isFightingAnimationPlaying = true;
        animator.SetBool("isFighting", false);
    }

    public void fightAnimationStopped()
    {
       isFightingAnimationPlaying = false;
        alreadyAttacked = false;
        player.GetComponent<PlayerHealth>().takeDamage(10);
    }
}
