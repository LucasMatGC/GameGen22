using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CalculatePriority : ActionNode
{

    public float maxTime = 80f;
    public float baseThreshold = 0.15f;

    private float firstTaskTime = 80f;
    private float secondTaskTime = 80f;
    private float thirdTaskTime = 80f;


    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        firstTaskTime -= Time.deltaTime;
        secondTaskTime -= Time.deltaTime;
        thirdTaskTime -= Time.deltaTime;

        if (firstTaskTime <= 0f)
        {

            blackboard.priorityTask = 1;
            firstTaskTime = maxTime;

        } else if (secondTaskTime <= 0f)
        {

            blackboard.priorityTask = 2;
            secondTaskTime = maxTime;

        } else if (thirdTaskTime <= 0f)
        {

            blackboard.priorityTask = 3;
            thirdTaskTime = maxTime;

        }
        else
        {

            blackboard.firstThreshold = baseThreshold + baseThreshold * (1 - (firstTaskTime / maxTime));
            blackboard.secondThreshold = blackboard.firstThreshold + baseThreshold + baseThreshold * (1 - (secondTaskTime / maxTime));
            blackboard.thirdThreshold = blackboard.secondThreshold + baseThreshold + baseThreshold * (1 - (thirdTaskTime / maxTime));
            blackboard.probability = Random.Range(0, 1);

        }


        return State.Success;
    }
}
