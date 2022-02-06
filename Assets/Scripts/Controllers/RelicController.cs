using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicController : MonoBehaviour
{
    public bool busy = false;

    public void watch(float booking)
    {
        if (busy == false)
        {
            busy = true;
            StartCoroutine(MyCoroutine(booking));
        }

    }

    IEnumerator MyCoroutine(float booking)
    {
        //Debug.Log("Viendo reliquia");
        yield return new WaitForSeconds(booking);
        //Debug.Log("Accion terminada");
        busy = false;
    }
}
