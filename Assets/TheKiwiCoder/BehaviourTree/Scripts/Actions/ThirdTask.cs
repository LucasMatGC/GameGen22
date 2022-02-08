using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ThirdTask : ActionNode
{
    public float timeOnTask = 0f;
    public float taskTime = 5f;
    public bool isExecuting = false;

    public float tolerance = 0.1f;

    private GameObject? task;
    private bool isActionActivated = false;

    protected override void OnStart()
    {

        timeOnTask = 0f;
        isExecuting = false;

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {

        if (!isExecuting && (blackboard.priorityTask == 3 || (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.thirdThreshold)))
        {

            if (blackboard.Tasks[2] != "sweep")
            {

                task = ActionsMaster.instance.IHaveTo(blackboard.Tasks[2]); 

                if (task == null)
                {

                    return State.Failure;
                    
                }

                context.agent.destination = (Vector3)task.transform.position;

            } else
            {

                context.broomGO.SetActive(true);

            }

            isExecuting = true;

        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        if (isExecuting)
        {

            if (context.agent.pathPending)
                return State.Running;

            if (context.agent.remainingDistance < tolerance)
            {

                if (!isActionActivated)
                {

                    ActionsMaster.instance.StartAction(blackboard.Tasks[2], task);
                    context.actionBubble.GetComponent<Animator>().SetTrigger(blackboard.Tasks[2]);
                    isActionActivated = true;

                }

                timeOnTask += Time.deltaTime;
            }

            if (timeOnTask < taskTime)
            {
                return State.Running;
            }
            else
            {

                isExecuting = false;
                context.actionBubble.GetComponent<Animator>().SetTrigger("stopActing");
                blackboard.thirdTaskTime = Time.time + blackboard.maxTime;

                if (context.broomGO.activeSelf)
                {

                    context.broomGO.SetActive(false);
                    return State.Success;

                }
                else
                {

                    blackboard.RandomPosition = new Vector3(Random.Range(blackboard.min.x, blackboard.max.x), 0f, Random.Range(blackboard.min.y, blackboard.max.y));
                    context.agent.destination = blackboard.RandomPosition;

                }


            }
        } else if (isActionActivated)
        {

            if (context.agent.pathPending)
                return State.Running;

            return State.Success;

        }

        return State.Failure;

    }
}
