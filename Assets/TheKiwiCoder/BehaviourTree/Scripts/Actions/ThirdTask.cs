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

            Debug.Log("Toca tercera regla! Ha sido por prioridad: " + (blackboard.priorityTask == 3) + ", ha sido por probabilidad: " + (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.thirdThreshold));
            Debug.Log("ThirdThreshold: " + blackboard.thirdThreshold+ ", probability: " + blackboard.probability);
            Debug.Log("La primera tarea es: " + blackboard.Tasks[2]);
            context.agent.destination = blackboard.PoisonPosition;
            isExecuting = true;


        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("IMPOSIBLE IR! THIRD TASK");
        }

        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
                timeOnTask += Time.deltaTime;
            }

            if (timeOnTask < taskTime)
            {
                //Debug.Log("Ejecutando tercera regla! timeOnTask: " + timeOnTask);
                return State.Running;
            }
            else
            {

                isExecuting = false;

                Debug.Log("Finalizada tercera regla!");
                blackboard.thirdTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
