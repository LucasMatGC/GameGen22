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

            Debug.Log("Toca segunda regla! Ha sido por prioridad: " + (blackboard.priorityTask == 2) + ", ha sido por probabilidad: " + (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.secondThreshold));
            Debug.Log("SecondThreshold: " + blackboard.secondThreshold + ", probability: " + blackboard.probability);
            Debug.Log("La primera tarea es: " + blackboard.Tasks[1]);
            context.agent.destination = blackboard.AltarPosition;
            isExecuting = true;


        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("IMPOSIBLE IR! SECODN TASK");
        }

        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
                timeOnTask += Time.deltaTime;
            }

            if (timeOnTask < taskTime)
            {
                //Debug.Log("Ejecutando segunda regla! timeOnTask: " + timeOnTask);
                return State.Running;
            }
            else
            {

                isExecuting = false;

                Debug.Log("Finalizada segunda regla!");
                blackboard.secondTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
