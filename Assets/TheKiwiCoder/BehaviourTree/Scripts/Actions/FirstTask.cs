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

    private GameObject? task;
    private bool isActionCalled;

    protected override void OnStart() {

        timeOnTask = 0f;
        isExecuting = false;

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (!isExecuting && (blackboard.priorityTask == 1 || (blackboard.priorityTask == 0 && blackboard.probability <= blackboard.firstThreshold)))
        {

            //Debug.Log("La primera tarea es: " + blackboard.Tasks[0]);

            if (blackboard.Tasks[0] != "sweep")
            {

                task = ActionsMaster.instance.IHaveTo(blackboard.Tasks[0]);

                if (task == null)
                {

                    return State.Failure;

                    //new WaitForSeconds(2f);
                    //destination = ActionsMaster.instance.IHaveTo(blackboard.Tasks[0]);

                }
                Debug.Log("La primera " + blackboard.Tasks[0] + " se esta ejecutando");
                context.agent.destination = (Vector3)task.transform.position;
                isExecuting = true;

            }
            else
            {
            
                context.broomGO.SetActive(true);
                isExecuting = true;

            }

        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            //Debug.LogWarning("IMPOSIBLE IR! FIRST TASK");
            return State.Failure;
        }


        if (isExecuting)
        {

            if (context.agent.remainingDistance <= tolerance)
            {
                if (isActionCalled)
                {

                    //ActionMaster.instance.startAction(blackboard.Tasks[0], task);

                    isActionCalled = true;

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

                //Debug.Log("Finalizada primera regla!");
                blackboard.firstTaskTime = Time.time + blackboard.maxTime;
                return State.Success;

            }
        }

        return State.Failure;

    }
}
