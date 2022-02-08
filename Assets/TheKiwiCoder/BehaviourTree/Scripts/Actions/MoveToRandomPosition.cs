using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToRandomPosition : ActionNode
{
    public float timeOnTask = 0f;
    public float taskTime = 5f;
    public float threshold = 1f;
    public bool isExecuting = false;

    public float tolerance = 1.0f;

    protected override void OnStart()
    {

        threshold = 1 - ((1 - blackboard.thirdThreshold) / 3);
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

            //Debug.Log("Toca movimiento aleatorio! Ha sido por probabilidad: " + (blackboard.priorityTask == 0 && blackboard.probability <= threshold));
            blackboard.RandomPosition = new Vector3(Random.Range(blackboard.min.x, blackboard.max.x), 0f, Random.Range(blackboard.min.y, blackboard.max.y));
            context.agent.destination = blackboard.RandomPosition;
            isExecuting = true;
            context.animator.enabled = true;
            context.sfx.enabled = true;


        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            //Debug.LogWarning("IMPOSIBLE IR! MOVEMENT TASK");
            return State.Failure;
        }

        if (isExecuting)
        {

            timeOnTask += Time.deltaTime;

            if (timeOnTask < taskTime)
            {
                //Debug.Log("Ejecutando movimiento aleatorio!");
                return State.Running;
            }
            else
            {

                isExecuting = false;
                context.animator.enabled = false;
                context.sfx.enabled = false;

                //Debug.Log("Finalizada movimiento aleatorio!");
                return State.Success;

            }
        }

        return State.Failure;

    }
}
