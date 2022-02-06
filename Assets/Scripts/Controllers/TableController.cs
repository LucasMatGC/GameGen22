using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public bool busy = false;
    public GameObject tableGeneral;
    public GameObject table;

    private void Start()
    {
        tableGeneral.GetComponent<ActionsGeneralController>().addActionToList(table);
    }

    public void read(float booking)
    {
        if (busy == false)
        {
            busy = true;
            tableGeneral.GetComponent<ActionsGeneralController>().markActionTaken(table); 
            //tableGeneral.GetComponent<ActionsGeneralController>().getFreeAction();
            StartCoroutine(MyCoroutine(booking));
        }

    }

    IEnumerator MyCoroutine(float booking)
    {
        //Debug.Log("Leyendo");
        yield return new WaitForSeconds(booking);
        //Debug.Log("Accion terminada");
        tableGeneral.GetComponent<ActionsGeneralController>().markActionFree(table);
        busy = false;
    }
}
