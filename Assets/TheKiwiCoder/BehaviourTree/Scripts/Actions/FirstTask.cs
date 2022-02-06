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

    protected override void OnStart() {

        timeOnTask = 0f;
        isExecuting = false;

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (!isExecuting && (blackboard.priorityTask == 1 || (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.firstThreshold)))
        {

            Debug.Log("Toca primera regla! Ha sido por prioridad: " + (blackboard.priorityTask == 1) + ", ha sido por probabilidad: " + (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.firstThreshold));
            Debug.Log("FirstThreshold: " + blackboard.firstThreshold + ", probability: " + blackboard.probability);
            Debug.Log("La primera tarea es: " + blackboard.Tasks[0]);
            context.agent.destination = blackboard.CandlePosition;
            isExecuting = true;


        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("IMPOSIBLE IR! FIRST TASK");
        }


        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
                timeOnTask += Time.deltaTime;
            }

        
            if (timeOnTask < taskTime)
            {
                //Debug.Log("Ejecutando primera regla! timeOnTask: " + timeOnTask);
                return State.Running;
            } else
            {

                isExecuting = false;

                Debug.Log("Finalizada primera regla!");
                blackboard.firstTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
