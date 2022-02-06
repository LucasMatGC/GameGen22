using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsGeneralController : MonoBehaviour
{
    List<(GameObject, bool)> actionsList = new List<(GameObject,bool)>();
    


    public void addActionToList(GameObject newAction)
    {
        actionsList.Add((newAction, true));
        Debug.Log("Se ha añadido un nuevo componente a la lista: " + newAction);
        Debug.Log("Total = " + actionsList.Count);
    }

    public Vector3? getFreeAction()
    {
        if (actionsList.Count != 0)
        {
            for (int i = 0; i< actionsList.Count;i++)
            {
                (GameObject, bool) action = actionsList[i];
                Debug.Log("Mesas hasta disponible: " + action);
                if (action.Item2 == true)
                {
                    action.Item2 = false;
                    actionsList[i] = action;
                    Debug.Log("Siguiente mesa libre "+ action);
                    markActionTaken(action.Item1);
                    return action.Item1.transform.position;
                }
            }
        }

        return null;
    }
    public void markActionTaken(GameObject actionTaken)
    {
        //(GameObject, bool) toFind = (tableTaken, true);
        int location = actionsList.FindIndex(x => x.Item1 = actionTaken);
        if (location != -1)
        {            
            actionsList[location] = (actionTaken, false);
            Debug.Log("Mesa reservada!!");
        }
        else
        {
            Debug.Log("Error in TableGeneralController, the element " + actionTaken + " is not in the list");
        }        
    }

    public void markActionFree(GameObject actionFree)
    {
        //(GameObject, bool) toFind = (tableTaken, true);
        int location = actionsList.FindIndex(x => x.Item1 = actionFree);
        if (location != -1)
        {
            actionsList[location] = (actionFree, true);
            Debug.Log("Mesa libre!!");
        }
        else
        {
            Debug.Log("Error in TableGeneralController, the element " + actionFree + " is not in the list");
        }
    }
}
