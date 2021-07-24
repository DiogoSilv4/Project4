using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionStatus {Ready, Running, Done};

public abstract class ActionBase : MonoBehaviour
{
    protected ActionStatus status;

    public abstract ActionStatus RunAction(ActionInfo actionInfo, Director director);
        
}
