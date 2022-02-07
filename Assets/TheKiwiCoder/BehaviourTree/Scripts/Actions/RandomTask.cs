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

    private Vector3? destination;

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

            //Debug.Log("La tarea aleatoria es: " + blackboard.Tasks[3]);

            if (blackboard.Tasks[3] != "sweep")
            {

                destination = ActionsMaster.instance.IHaveTo(blackboard.Tasks[3]).transform.position;

                if (destination == null)
                {

                    return State.Failure;
                    
                    //new WaitForSeconds(2f);
                    //destination = ActionsMaster.instance.IHaveTo(blackboard.Tasks[3]);

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
            //Debug.LogWarning("IMPOSIBLE IR! RANDOM TASK");
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

                //Debug.Log("Finalizada tarea aleatoria!");
                
                return State.Success;

            }
        }

        return State.Failure;

    }
}
