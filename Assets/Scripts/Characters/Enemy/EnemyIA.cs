using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    public bool Tornado_On;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")] 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public int Damage;
    public GameObject Player;
    public Animator Anim;
    public Animator Img_Anim;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Enemy Type")]
    public bool isMelee;
    public bool isRange;

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
        Img_Anim = GameObject.FindWithTag("Player_Img").GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMelee == true && isRange == false)
        {
            sightRange = 2.5f;
            attackRange = 1f;
        }

        if (isRange == true && isMelee == false)
        {
            sightRange = 5;
            attackRange = 4;
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        Tornado_On = Player.GetComponent<Player_Abilities>().Tornado_Abilty;
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if(Tornado_On == false)
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        if(Tornado_On == false)
        {
            //Make sure enemy doesn't move
            agent.SetDestination(transform.position);

           

            if (!alreadyAttacked)
            {
                if(isRange == true && isMelee == false)
                {
                    transform.LookAt(player);
                    Vector3 offsete = new Vector3(0, 0f, 0.5f);
                    //var insta = Instantiate(Fuego_H1, transform.position + offsete, Fuego_H1.transform.rotation);
                   
                    Rigidbody rb = Instantiate(projectile, transform.position + offsete, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                }

                if(isMelee == true && isRange == false)
                {
                    Anim.SetTrigger("EAttack");
                    transform.LookAt(player);
                    
                    IEnumerator ExecuteAfterTime()
                    {
                        yield return new WaitForSeconds(0.7f);

                        Img_Anim.SetTrigger("Img_Damage");
                        Player.GetComponent<HealthManager>().DamageCharacter(Damage);
                    }

                    StartCoroutine(ExecuteAfterTime());
                }

                ///End of attack code

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
