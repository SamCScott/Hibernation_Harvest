using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyControl : MonoBehaviour
{
    public float range = 5f;
    NavMeshAgent agent;
    public Transform target, home;

    public Animator bearAnim;
    public bool chase;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bearAnim = gameObject.GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        chase = false;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.pickUp[2] >= 1)
        {
            range = GameManager.Instance.pickUp[2] * 2;
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= range)
        {
            agent.SetDestination(target.position);
            chase = true;
            if (distance <= agent.stoppingDistance)
            {
                //face the target and attack
                TurnToTarget();
            }
        }
        else
        {
            GoHome(distance);
        }
        if(chase == false)
        {
            bearAnim.SetBool("chase", false);
        }
        else //if(chase == true)
        {
            bearAnim.SetBool("chase", true);
        }
    }

    void GoHome(float distance)
    {
        // Set the agent to go to the currently selected destination.
        if (distance > range)
        {
            agent.SetDestination(home.position);            
            chase = false;
        }        
    }

    void TurnToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag.ToString())
        {
            case "Player":
                GameManager.Instance.playerHealth -= 1;
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
