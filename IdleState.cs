using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour
{
    
    NavMeshAgent agent;
    Transform player;
    Transform weapon;
    
    float attackRange = 2.5f;
    float weaponRange = 2.0f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Gets Nav mesh for AI as well as identifying the player
        
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weapon = GameObject.FindGameObjectWithTag("Weapon").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Gets distance from the player which the AI will chase until the player is out of the chase range
        
        float distance = Vector3.Distance(animator.transform.position, player.position);

        float weaponDist = Vector3.Distance(animator.transform.position, weapon.position);

        // AI will begin to attack player if they're close enough to the player
        
        if(distance < attackRange)
        {
            animator.SetBool("attackPlayer", true);
            animator.SetBool("moveToPlayer", false);
        }
        else
        {
            animator.SetBool("moveToPlayer", true);
            animator.SetBool("attackPlayer", false);
        }

        if(weaponDist < weaponRange)
        {
            animator.SetBool("aiDead", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

}

