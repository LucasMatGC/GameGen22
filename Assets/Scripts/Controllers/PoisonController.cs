using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : MonoBehaviour
{
    public bool busy = false;
    public AudioSource sfx;

    public void Poison(float booking)
    {
        if (busy == false)
        {
            busy = true;
            StartCoroutine(MyCoroutine(booking));
        }

    }

    IEnumerator MyCoroutine(float booking)
    {
        //Debug.Log("Rezando");
        yield return new WaitForSeconds(booking);
        sfx.Play();
        //Debug.Log("Accion terminada");
        busy = false;
    }
}