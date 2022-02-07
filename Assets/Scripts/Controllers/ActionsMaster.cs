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

    public static ActionsMaster instance;

    string[] actions = { "read", "pray", "watch", "sweep", "lighter" };
    string[] selectedActions = { "", "", "" };
    string[] optionalActions;

    bool prayAssigned = false;
    bool watchAssigned = false;
    // Start is called before the first frame update
    void Start()
    {

        instance = this;

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
        string[] newActions = { selectedActions[0], selectedActions[1], selectedActions[2], optionalActions[i] };
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
                    StartCoroutine(WaitPray(3f));
                    return pray.gameObject;
                }
                break;
            case "watch":
                if (!watchAssigned && !relic.gameObject.GetComponent<RelicController>().busy)
                {
                    watchAssigned = true;
                    StartCoroutine(WaitWatch(3f));
                    return relic.gameObject;
                }
                break;
            case "lighter":
                return candles.gameObject.GetComponent<ActionsGeneralController>().getFreeAction();
                //break;
        }
        return null;
    }
    
    void startAction(string currentTask, GameObject other)
    {
        
        switch (currentTask)
        {
            case "lighter":
                //Debug.Log("Encendiendo... vela");
                actionTime = 5f;
                other.GetComponent<CandleController>().lightCandle(actionTime);
                break;
            case "altar":
                //Debug.Log("Rezando...");
                actionTime = 5f;
                other.GetComponent<PrayController>().pray(actionTime);
                break;
            case "book":
                //Debug.Log("Leyendo...");
                actionTime = 5f;
                other.GetComponent<TableController>().read(actionTime);
                break;
            case "relic":
                //Debug.Log("Viendo la reliquia...");
                actionTime = 5f;
                other.GetComponent<RelicController>().watch(actionTime);
                break;
            case "sweep":
                //Debug.Log("Barriendo...");
                actionTime = 5f;
                break;
        }
        
    }

    IEnumerator WaitPray(float time)
    {
        //prayAssigned = true;
        yield return new WaitForSeconds(time);
        prayAssigned = false;
    }
    IEnumerator WaitWatch(float time)
    {
        //watchAssigned = true;
        yield return new WaitForSeconds(time);
        watchAssigned = false;
    }
}
