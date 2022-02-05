using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayController : MonoBehaviour
{
    public bool ocupado = false;

    public void rezar(float reserva)
    {
        if (ocupado == false)
        {
            ocupado = true;
            StartCoroutine(MyCoroutine(reserva));
        }

    }

    IEnumerator MyCoroutine(float reserva)
    {
        Debug.Log("Rezando");
        yield return new WaitForSeconds(reserva);
        Debug.Log("Accion terminada");
        ocupado = false;
    }
}
