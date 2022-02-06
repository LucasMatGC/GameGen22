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

            Debug.Log("Toca tarea aleatoria! Ha sido por probabilidad: " + (blackboard.priorityTask == 0 && blackboard.probability <= threshold));
            int randomTaskNumer = Random.Range(3, 5);
            Debug.Log("La tarea aleatoria es: " + blackboard.Tasks[randomTaskNumer]);

            if (blackboard.Tasks[randomTaskNumer] == "Book")
            {

                context.agent.destination = blackboard.BookPosition;

            } else
            {

                context.broomGO.SetActive(true);

            }

            isExecuting = true;


        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("IMPOSIBLE IR! RANDOM TASK");
        }

        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
                timeOnTask += Time.deltaTime;
            }

            if (timeOnTask < taskTime)
            {
                //Debug.Log("Ejecutando tarea aleatoria!");
                return State.Running;
            }
            else
            {

                isExecuting = false;

                if (context.broomGO.activeSelf)
                {

                    context.broomGO.SetActive(false);

                }

                Debug.Log("Finalizada tarea aleatoria!");
                
                return State.Success;

            }
        }

        return State.Failure;

    }
}
