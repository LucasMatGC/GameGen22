using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CalculatePriority : ActionNode
{

    public float baseThreshold = 0.15f;

    protected override void OnStart() {

        if (!ActionsMaster.instance.AreNewActions() && blackboard.Tasks.Length == 0)
        {

            UpdateTaskTime(1);
            UpdateTaskTime(2);
            UpdateTaskTime(3);
            blackboard.Tasks = ActionsMaster.instance.GiveMeActions();

        }

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (blackboard.firstTaskTime <= Time.time)
        {

            blackboard.priorityTask = 1;
            UpdateTaskTime(1);

        } else if (blackboard.secondTaskTime <= Time.time)
        {

            blackboard.priorityTask = 2;
            UpdateTaskTime(2);

        } else if (blackboard.thirdTaskTime <= Time.time)
        {

            blackboard.priorityTask = 3;
            UpdateTaskTime(3);

        }
        else
        {

            blackboard.priorityTask = 0;
            blackboard.firstThreshold = baseThreshold + baseThreshold * (1 - (blackboard.firstTaskTime / blackboard.maxTime));
            blackboard.secondThreshold = blackboard.firstThreshold + baseThreshold + baseThreshold * (1 - (blackboard.secondTaskTime / blackboard.maxTime));
            blackboard.thirdThreshold = blackboard.secondThreshold + baseThreshold + baseThreshold * (1 - (blackboard.thirdTaskTime / blackboard.maxTime));
            blackboard.probability = Random.Range(0f, 1f);

        }

        return State.Success;
    }

    private void UpdateTaskTime(int rule)
    {

        switch (rule)
        {

            case 1:
                blackboard.firstTaskTime = Time.time + blackboard.maxTime;
                break;

            case 2:
                blackboard.secondTaskTime = Time.time + blackboard.maxTime;
                break;

            case 3:
                blackboard.thirdTaskTime = Time.time + blackboard.maxTime;
                break;

            default:
                break;

        }

    }

}
