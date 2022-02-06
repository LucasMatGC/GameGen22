using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CalculatePriority : ActionNode
{

    public float baseThreshold = 0.15f;

    protected override void OnStart() {

        if (blackboard.firstTaskTime == 0f)
        {

            UpdateTaskTime(1);
            UpdateTaskTime(2);
            UpdateTaskTime(3);
            blackboard.Tasks = new string[5] {"Candle", "Altar", "Poison", "Book", "Sweep"};
            blackboard.BookPosition = new Vector3(-1.55f, 0f, 12f);
            blackboard.AltarPosition = new Vector3(1.34f, 0f, -13.68f);
            blackboard.CandlePosition = new Vector3(10.38f, 0f, 8.18f);
            blackboard.PoisonPosition = new Vector3(-10.34f, 0f, -2.19f);
        }

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        //blackboard.firstTaskTime -= Time.deltaTime;
        //blackboard.secondTaskTime -= Time.deltaTime;
        //blackboard.thirdTaskTime -= Time.deltaTime;

        if (blackboard.firstTaskTime <= Time.time)
        {

            blackboard.priorityTask = 1;
            UpdateTaskTime(1);
            //blackboard.firstTaskTime = maxTime;

        } else if (blackboard.secondTaskTime <= Time.time)
        {

            blackboard.priorityTask = 2;
            UpdateTaskTime(2);
            //blackboard.secondTaskTime = maxTime;

        } else if (blackboard.thirdTaskTime <= Time.time)
        {

            blackboard.priorityTask = 3;
            UpdateTaskTime(3);
            //blackboard.thirdTaskTime = maxTime;

        }
        else
        {

            blackboard.priorityTask = 0;
            blackboard.firstThreshold = baseThreshold + baseThreshold * (1 - (blackboard.firstTaskTime / blackboard.maxTime));
            blackboard.secondThreshold = blackboard.firstThreshold + baseThreshold + baseThreshold * (1 - (blackboard.secondTaskTime / blackboard.maxTime));
            blackboard.thirdThreshold = blackboard.secondThreshold + baseThreshold + baseThreshold * (1 - (blackboard.thirdTaskTime / blackboard.maxTime));
            blackboard.probability = Random.Range(0f, 1f);

        }

        Debug.Log("Calculo de prioridad! firstThreshold: " + blackboard.firstThreshold + ", secondThreshold: " + blackboard.secondThreshold + ", thirdThreshold: " + blackboard.thirdThreshold + ", probability: " + blackboard.probability + ", priorityTask: " + blackboard.priorityTask);
        Debug.Log("Tiempos de tareas! firstTaskTime: " + blackboard.firstTaskTime+ ", secondTaskTime: " + blackboard.secondTaskTime + ", thirdTaskTime: " + blackboard.thirdTaskTime + ", actualTime: " + Time.time);


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
