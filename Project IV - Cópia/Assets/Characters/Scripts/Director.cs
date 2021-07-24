using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ActorInfo
{
    public string name;
    public GameObject gameobject;
}

[Serializable]
public class LocationInfo
{
    public string name;
    public Transform transform;
}

[Serializable]
public class ActionInfo
{
    public string name;
    public string[] parameters;
    public ActionStatus status;
}

public class Director : MonoBehaviour
{
    //ACTIONS: Go, Idle, Attack, Talk

    //CHACTERS: actor1, actor2

    //PLACES: place1, place2

    //PLOT: talk(actor1, actor2, blabla), talk(actor2, actor1, blabla), go(actor1, place2), go(actor2, place2),
    //talk(actor1, actor2, blabla), talk(actor2, actor1, blabla), attack(actor1, actor2), attack(actor2, actor1)
    //

    public ActorInfo[] actors;
    public LocationInfo[] places;
    public ActionInfo[] plot;
    private int currentEventIndex = 0;
    private float talkTimer = 0;


    public Transform GetLocationTransform(string name)
    {
        foreach (LocationInfo location in places)
        {
            if (location.name == name)
            {
                return location.transform;
            }
        }
        return null;
    }

    public GameObject GetActorGameObject(string name)
    {
        foreach (ActorInfo actor in actors)
        {
            if (actor.name == name)
            {
                return actor.gameobject;
            }
        }
        return null;
    }

    void Update()
    {
        if (plot[currentEventIndex].status == ActionStatus.Ready)
        {
            plot[currentEventIndex].status = ActionStatus.Running;
        }
        else if (plot[currentEventIndex].status == ActionStatus.Running)
        {
            if (plot[currentEventIndex].name == "go")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<GoAction>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "talk")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<TalkAction>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "yes")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<Acknowledged>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "cry")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<CryAction>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "dance")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<DanceAction>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "look")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<LookAction>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "push_button")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<buttonAction>().RunAction(plot[currentEventIndex], this);
            }
            else if (plot[currentEventIndex].name == "head_no")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<headNodAction>().RunAction(plot[currentEventIndex], this);
            }
            //rage, knocked, pray, surprised, push
            else if (plot[currentEventIndex].name == "last")
            {
                GameObject actor = GetActorGameObject(plot[currentEventIndex].parameters[0]);
                plot[currentEventIndex].status = actor.GetComponent<Actionss>().RunAction(plot[currentEventIndex], this);
            }
            else
            {
                 if (plot[currentEventIndex].status == ActionStatus.Ready)
                {            
                    talkTimer = 0;
                    plot[currentEventIndex].status = ActionStatus.Running;
                }
                else if (plot[currentEventIndex].status == ActionStatus.Running)
                {
                    talkTimer = talkTimer + Time.deltaTime;
                    if (talkTimer > 5)
                    {
                        plot[currentEventIndex].status = ActionStatus.Done;
                    }            
                }
                // plot[currentEventIndex].status = ActionStatus.Done;

                Debug.Log("Action " + plot[currentEventIndex].name + " done!");
            }
        }
        else if (plot[currentEventIndex].status == ActionStatus.Done)
        {
            if (currentEventIndex < plot.Length - 1)
            {
                // StartCoroutine("StartMovementx");
                currentEventIndex++;

            }            
        }


    }
     private IEnumerator StartMovementx(){

        yield return new WaitForSeconds(1);
                currentEventIndex++;

    }
}
