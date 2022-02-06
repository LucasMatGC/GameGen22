using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ThirdTask : ActionNode
{
    public float timeOnTask = 0f;
    public float taskTime = 5f;
    public bool isExecuting = false;

    public float tolerance = 0.01f;

    private Vector3? destination;

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

            //Debug.Log("La primera tarea es: " + blackboard.Tasks[2]);

            if (blackboard.Tasks[2] != "sweep")
            {

                destination = ActionsMaster.instance.IHaveTo(blackboard.Tasks[2]);

                while (destination == null)
                {
                    new WaitForSeconds(2f);
                    destination = ActionsMaster.instance.IHaveTo(blackboard.Tasks[2]);

                }

                context.agent.destination = (Vector3)destination;
                isExecuting = true;

            } else
            {

                context.broomGO.SetActive(true);
                isExecuting = true;

            }


    }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            //Debug.LogWarning("IMPOSIBLE IR! THIRD TASK");
            return State.Failure;
        }

        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
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

                //Debug.Log("Finalizada tercera regla!");
                blackboard.thirdTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
