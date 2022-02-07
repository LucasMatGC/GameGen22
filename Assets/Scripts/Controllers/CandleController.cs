using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public bool busy = false;

    public GameObject candleGeneral;
    public GameObject candle;
    public GameObject flame;

    private void Start()
    {
        candleGeneral.GetComponent<ActionsGeneralController>().addActionToList(candle);
    }

    public void lightCandle(float booking)
    {
        if (busy == false)
        {
            busy = true;
            candleGeneral.GetComponent<ActionsGeneralController>().markActionTaken(candle);
            StartCoroutine(MyCoroutine(booking));
        }
    
    }

    IEnumerator MyCoroutine(float booking)
    {
        yield return new WaitForSeconds(booking);
        //Debug.Log("Vela encendida");
        var rand = new System.Random();
        int time = rand.Next(7, 13);
        flame.SetActive(true);
        yield return new WaitForSeconds(time);
        flame.SetActive(false);
        //Debug.Log("Vela apagada");
        candleGeneral.GetComponent<ActionsGeneralController>().markActionFree(candle);
        busy = false;
    }
}
