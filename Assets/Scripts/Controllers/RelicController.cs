using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicController : MonoBehaviour
{
    public bool ocupado = false;

    public void ver(float reserva)
    {
        if (ocupado == false)
        {
            ocupado = true;
            StartCoroutine(MyCoroutine(reserva));
        }

    }

    IEnumerator MyCoroutine(float reserva)
    {
        Debug.Log("Viendo reliquia");
        yield return new WaitForSeconds(reserva);
        Debug.Log("Accion terminada");
        ocupado = false;
    }
}
