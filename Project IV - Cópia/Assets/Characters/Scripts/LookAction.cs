using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAction : ActionBase
{
    private Animator animator;
    private float talkTimer = 0;

    public override ActionStatus RunAction(ActionInfo actionInfo, Director director)
    {
        if (status == ActionStatus.Ready)
        {            
            animator = GetComponent<Animator>();
            animator.SetBool("lookBack", true);
            talkTimer = 0;
            status = ActionStatus.Running;
        }
        else if (status == ActionStatus.Running)
        {
            talkTimer = talkTimer + Time.deltaTime;
            if (talkTimer > 10)
            {
                animator.SetBool("lookBack", false);
                status = ActionStatus.Done;
            }            
        }
        return status;
    }
}
