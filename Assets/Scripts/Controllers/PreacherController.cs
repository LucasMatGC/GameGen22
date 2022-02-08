using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreacherController : MonoBehaviour
{
    public bool busy = false;

    public void Speak(float booking)
    {
        if (busy == false)
        {
            busy = true;
            StartCoroutine(MyCoroutine(booking));
        }

    }

    IEnumerator MyCoroutine(float booking)
    {
        yield return new WaitForSeconds(booking);
        busy = false;
    }
}