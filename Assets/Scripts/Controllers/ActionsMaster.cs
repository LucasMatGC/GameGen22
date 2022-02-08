using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionsMaster : MonoBehaviour
{
    public GameObject tables;
    public GameObject pray;
    public GameObject candles;
    public GameObject relic;
    public GameObject poison;
    public GameObject preacher;

    public static ActionsMaster instance;

    string[] actions = { "read", "pray", "watch", "sweep", "lighter","poison","speak" };
    string[] selectedActions = { "", "", "" };
    string[] optionalActions;
    bool seedyOption = false;

    bool prayAssigned = false;
    bool watchAssigned = false;
    bool poisonAssigned = false;
    bool preacherAssigned = false;

    private int counterNPCs = 0;
    // Start is called before the first frame update
    void Start()
    {

        instance = this;

        counterNPCs = 0;

        var rnd = new System.Random();
        int first = rnd.Next(5);
        int second = rnd.Next(4);
        int third = rnd.Next(3);

        if (first <= second)
        {
            second++;
            if (first <= third)
            {
                third++;
            }
            if (second <= third)
            {
                third++;
            }
        }
        else
        {
            if (second <= third)
            {
                third++;
            }
            if (first <= third)
            {
                third++;
            }
        }
        
        selectedActions[0] = actions[first];
        selectedActions[1] = actions[second];
        selectedActions[2] = actions[third];
        optionalActions = actions.Except(selectedActions).ToArray();
        
    }

    public string[] GiveMeActions()
    {
        var rnd = new System.Random();
        int i = rnd.Next(2);
        string optional;
        if (!seedyOption)
        {
            optional = optionalActions[i];  
        }
        else
        {
            optional = optionalActions[i+2];            
        }
        seedyOption = !seedyOption;
        string[] newActions = { selectedActions[0], selectedActions[1], selectedActions[2], optional };
        counterNPCs++;
        return newActions;
    }

    public GameObject? IHaveTo(string action)
    {
        //Debug.Log("action by NPC " + action);
        switch (action)
        {
            case "read":
                return tables.gameObject.GetComponent<ActionsGeneralController>().getFreeAction();
                //break;
            case "pray":
                if (!prayAssigned && !pray.gameObject.GetComponent<PrayController>().busy)
                {
                    prayAssigned = true;
                    StartCoroutine(Wait("pray",3f));
                    return pray.gameObject;
                }
                break;
            case "watch":
                if (!watchAssigned && !relic.gameObject.GetComponent<RelicController>().busy)
                {
                    watchAssigned = true;
                    StartCoroutine(Wait("watch",3f));
                    return relic.gameObject;
                }
                break;
            case "speak":
                if (!preacherAssigned && !preacher.gameObject.GetComponent<PreacherController>().busy)
                {
                    preacherAssigned = true;
                    StartCoroutine(Wait("speak",3f));
                    return preacher.gameObject;
                }
                break;
            case "poison":
                if (!poisonAssigned && !poison.gameObject.GetComponent<PoisonController>().busy)
                {
                    poisonAssigned = true;
                    StartCoroutine(Wait("poison", 3f));
                    return poison.gameObject;
                }
                break;
            case "lighter":
                return candles.gameObject.GetComponent<ActionsGeneralController>().getFreeAction();
                //break;
        }
        return null;
    }
    
    public void StartAction(string currentTask, GameObject other)
    {
        float actionTime;
        switch (currentTask)
        {
            case "lighter":
                //Debug.Log("Encendiendo... vela");
                actionTime = 5f;
                other.GetComponent<CandleController>().lightCandle(actionTime);
                break;
            case "pray":
                //Debug.Log("Rezando...");
                actionTime = 5f;
                other.GetComponent<PrayController>().pray(actionTime);
                break;
            case "read":
                //Debug.Log("Leyendo...");
                actionTime = 5f;
                other.GetComponent<TableController>().read(actionTime);
                break;
            case "watch":
                //Debug.Log("Viendo la reliquia...");
                actionTime = 5f;
                other.GetComponent<RelicController>().watch(actionTime);
                break;
            case "speak":
                //Debug.Log("Leyendo...");
                actionTime = 5f;
                other.GetComponent<PreacherController>().Speak(actionTime);
                break;
            case "poison":
                //Debug.Log("Viendo la reliquia...");
                actionTime = 5f;
                other.GetComponent<PoisonController>().Poison(actionTime);
                break;
            case "sweep":
                break;
        }
        
    }

    IEnumerator Wait(string currentTask, float time)
    {
        switch (currentTask)
        {            
            case "pray":
                yield return new WaitForSeconds(time);
                prayAssigned = false;
                break;
            
            case "watch":
                yield return new WaitForSeconds(time);
                watchAssigned = false;
                break;
            case "speak":
                yield return new WaitForSeconds(time);
                poisonAssigned = false;
                break;
            case "poison":
                yield return new WaitForSeconds(time);
                preacherAssigned = false;
                break;
        }
    }

    public bool AreNewActions()
    {

        return counterNPCs == 12;

    }

    public List<string> GetMandatoryTasks()
    {

        return new List<string>{ selectedActions[0], selectedActions[1], selectedActions[2] };

    }
}
