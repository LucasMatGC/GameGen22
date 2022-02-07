using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandomTask : ActionNode
{
    public float timeOnTask = 0f;
    public float taskTime = 5f;
    public float threshold = 1f;
    public bool isExecuting = false;

    public float tolerance = 0.01f;

    private GameObject? task;
    private bool isActionActivated = false;

    protected override void OnStart()
    {

        threshold = 1 - 2*((1 - blackboard.thirdThreshold)/3);
        timeOnTask = 0f;
        isExecuting = false;

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {

        if (!isExecuting && blackboard.probability <= threshold)
        {

            if (blackboard.Tasks[3] != "sweep")
            {

                task = ActionsMaster.instance.IHaveTo(blackboard.Tasks[3]);

                if (task == null)
                {

                    return State.Failure;

                }

                context.agent.destination = (Vector3)task.transform.position;
                isExecuting = true;

            } else
            {

                context.broomGO.SetActive(true);
                isExecuting = true;

            }


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

                    ActionsMaster.instance.StartAction(blackboard.Tasks[3], task);
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

                if (context.broomGO.activeSelf)
                {

                    context.broomGO.SetActive(false);

                }
                                
                return State.Success;

            }
        }

        return State.Failure;

    }
}
