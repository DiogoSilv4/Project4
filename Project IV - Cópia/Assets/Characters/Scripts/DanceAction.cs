using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceAction : ActionBase
{
    private Animator animator;
    private float talkTimer = 0;

    public override ActionStatus RunAction(ActionInfo actionInfo, Director director)
    {
        if (status == ActionStatus.Ready)
        {            
            animator = GetComponent<Animator>();
            animator.SetBool("CanDance", true);
            talkTimer = 0;
            status = ActionStatus.Running;
        }
        else if (status == ActionStatus.Running)
        {
            talkTimer = talkTimer + Time.deltaTime;
            if (talkTimer > 5)
            {
                animator.SetBool("CanDance", false);
                status = ActionStatus.Done;
            }            
        }
        return status;
    }
}
