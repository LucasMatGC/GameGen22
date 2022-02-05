using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public bool ocupado = false;

    public void leer(float reserva)
    {
        if (ocupado == false)
        {
            ocupado = true;
            StartCoroutine(MyCoroutine(reserva));
        }

    }

    IEnumerator MyCoroutine(float reserva)
    {
        Debug.Log("Leyendo");
        yield return new WaitForSeconds(reserva);
        Debug.Log("Accion terminada");
        ocupado = false;
    }
}
