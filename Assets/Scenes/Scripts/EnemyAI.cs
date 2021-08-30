using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target = null;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float turnSpeed = 10f;

    EnemyHealth enemyHealth;
    [SerializeField] NavMeshAgent navMeshAgent = null;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
       // navMeshAgent = GetComponent<NavMeshAgent>();   
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.IsDead()) {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
            EngageTarget();
        else if (distanceToTarget <= chaseRange)
            isProvoked = true;       
    }

    void EngageTarget() {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }
    public void OnDamageTaken() {
        isProvoked = true;
    }
    private void ChaseTarget() {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }
    private void AttackTarget() {
        GetComponent<Animator>().SetBool("Attack", true);
        Debug.Log(name + " has seeked and is destroying " + target.name);
    }

    private void FaceTarget() {
        //normalize because we dont want the distance or magnitude
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        //Slerp allows to rotate smoothly between two vectors
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
