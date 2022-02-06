using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionsMaster : MonoBehaviour
{
    string[] actions = { "read", "pray", "watch", "sweep", "lighter" };
    string[] selectedActions = { "", "", "" };
    string[] optionalActions;
    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();
        int first = rnd.Next(5);
        int second = rnd.Next(4);
        int therd = rnd.Next(3);

        if(first<= second)
        {
            second++;
        }
        if(first <= therd)
        {
            therd++;
        }
        if (second <= therd)
        {
            therd++;
        }
        selectedActions[0] = actions[first];
        selectedActions[1] = actions[second];
        selectedActions[2] = actions[therd];
        optionalActions = actions.Except(selectedActions).ToArray();
        foreach (string s in selectedActions)
        {
            Debug.Log("selectedActions = " + s);
        }
            
        Debug.Log("optionalActions = " + optionalActions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void giveMeActions()
    {
        var rnd = new System.Random();
        int i = rnd.Next(2);
        string[] newActions = { selectedActions[0], selectedActions[1], selectedActions[2], optionalActions[i] };
    }
}
