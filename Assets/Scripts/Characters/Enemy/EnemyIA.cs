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

    public AudioSource Asource;
    public AudioClip damage;
    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        Player = GameObject.FindWithTag("Player");
        Img_Anim = GameObject.FindWithTag("Player_Img").GetComponent<Animator>();

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

    //This makes the "NPC" move automatically through the room according to the next point to which it is directed
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

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

    //Sends the NPC towards the player, and if it is in attack range, it will attack the player.
    private void AttackPlayer()
    {
        if(Tornado_On == false)
        {
            //Make sure enemy doesn't move
            agent.SetDestination(transform.position);

           

            if (!alreadyAttacked)
            {
                if(isMelee == true && isRange == false)
                {
                    Anim.SetTrigger("EAttack");
                    transform.LookAt(player);
                    
                    IEnumerator ExecuteAfterTime()
                    {
                        yield return new WaitForSeconds(0.7f);

                        Asource.PlayOneShot(damage);
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
