using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public bool velaEncendida = false;
    
    public void encenderVela(float tiempoDeEncendido)
    {
        if (velaEncendida == false)
        {
            velaEncendida = true;
            StartCoroutine(MyCoroutine(tiempoDeEncendido));
        }
    
    }

    IEnumerator MyCoroutine(float tiempoDeEncendido)
    {
        yield return new WaitForSeconds(tiempoDeEncendido);
        Debug.Log("Vela encendida");
        var rand = new System.Random();
        int time = rand.Next(7, 13);
        yield return new WaitForSeconds(time);
        Debug.Log("Vela apagada");
        velaEncendida = false;
    }
}
