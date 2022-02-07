using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SecondTask : ActionNode
{
    public float timeOnTask = 0f;
    public float taskTime = 5f;
    public bool isExecuting = false;

    public float tolerance = 0.01f;

    private GameObject? task;
    private bool isActionCalled;

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

        if (!isExecuting && (blackboard.priorityTask == 2 || (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.secondThreshold)))
        {

            //Debug.Log("La primera tarea es: " + blackboard.Tasks[1]);

            if (blackboard.Tasks[1] != "sweep")
            {

                task = ActionsMaster.instance.IHaveTo(blackboard.Tasks[1]);

                if (task == null)
                {

                    return State.Failure;
                    
                    //new WaitForSeconds(2f);
                    //destination = ActionsMaster.instance.IHaveTo(blackboard.Tasks[1]);

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
            //Debug.LogWarning("IMPOSIBLE IR! SECODN TASK");
            return State.Failure;
        }

        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
                if (isActionCalled)
                {

                    //ActionMaster.instance.startAction(blackboard.Tasks[1], task);

                    isActionCalled = true;

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

                //Debug.Log("Finalizada segunda regla!");
                blackboard.secondTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
