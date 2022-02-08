using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class FirstTask : ActionNode
{

    public float timeOnTask = 0f;
    public float taskTime = 5f;
    public bool isExecuting = false;

    public float tolerance = 0.01f;

    private GameObject? task = null;
    private bool isActionActivated = false;

    protected override void OnStart() {

        timeOnTask = 0f;
        isExecuting = false;

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (!isExecuting && (blackboard.priorityTask == 1 || (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.firstThreshold)))
        {


            if (blackboard.Tasks[0] != "sweep")
            {

                task = ActionsMaster.instance.IHaveTo(blackboard.Tasks[0]);

                if (task == null)
                {

                    return State.Failure;

                }

                context.agent.destination = (Vector3)task.transform.position;
                isExecuting = true;

            }
            else
            {
            
                context.broomGO.SetActive(true);
                isExecuting = true;

            }
            context.actionBubble.GetComponent<Animator>().SetTrigger(blackboard.Tasks[0]);
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }


        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {

                if (!isActionActivated)
                {

                    ActionsMaster.instance.StartAction(blackboard.Tasks[0], task);
                    isActionActivated = true;

                }

                timeOnTask += Time.deltaTime;
            }

        
            if (timeOnTask < taskTime)
            {
                return State.Running;
            } else
            {

                isExecuting = false;

                if (context.broomGO.activeSelf)
                {

                    context.broomGO.SetActive(false);

                }
                
                context.actionBubble.GetComponent<Animator>().SetTrigger("stopActing");

                blackboard.firstTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
