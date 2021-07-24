using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAction : ActionBase
{    
    public float moveSpeed = 0.1f;
    public float maxSpeed = 0.1f;
    public float stopDistance = 1.5f;
    private Transform targetPosition;
    private Transform lookPosition;
    private CharacterController characterController;
    private Animator animator;

    public override ActionStatus RunAction(ActionInfo actionInfo, Director director)
    {
        // status = ActionStatus.Ready;
        if (status == ActionStatus.Ready)
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            targetPosition = director.GetLocationTransform(actionInfo.parameters[1]);
            status = ActionStatus.Running;
        }
        else if (status == ActionStatus.Running)
        {
            targetPosition = director.GetLocationTransform(actionInfo.parameters[1]);
            float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                              new Vector3(transform.position.x, 0, transform.position.z));

            if (distance <= stopDistance)
            {
                distance = 10;
                status = ActionStatus.Done;
                animator.SetFloat("Speed", 0);
            }
        }
        if(actionInfo.parameters.Length > 2){
            lookPosition = director.GetLocationTransform(actionInfo.parameters[2]);
        }
        return status;
    }

    private void Update()
    {
        if (status == ActionStatus.Running)
        {
            float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                          new Vector3(transform.position.x, 0, transform.position.z));
            if (distance  / 2.5f < 1)
            {
                animator.SetFloat("Speed", distance / 2.5f);
            }
            else
            {
                animator.SetFloat("Speed", 1);
            }
            if (distance  / 2.5f < 0.06f)
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (status == ActionStatus.Running)
        {
            float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                              new Vector3(transform.position.x, 0, transform.position.z));
            if (distance > 0.1f)
            {
                Vector3 direction = Vector3.Normalize(targetPosition.position - transform.position);
                if (distance < 2.5f)
                {
                    if (characterController.velocity.magnitude < maxSpeed)
                    {
                        characterController.SimpleMove(direction * (distance / 2));
                    }
                }
                else
                {
                    if (characterController.velocity.magnitude < maxSpeed)
                    {
                        characterController.SimpleMove(direction * moveSpeed);
                    }
                }
                Vector3 lookAtPosition = new Vector3(targetPosition.position.x, transform.position.y, targetPosition.position.z);
                if(lookPosition != null){
                     lookAtPosition = new Vector3(lookPosition.position.x, transform.position.y, lookPosition.position.z);
                }
                
                transform.LookAt(lookAtPosition);
            }
        }
    }
}
