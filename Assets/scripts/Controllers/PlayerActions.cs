using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject player;
    bool encendedor = false;
    Collider encendedorCollider;

    void Update()
    {
        if (encendedor == true && Input.GetButtonDown("Action"))
        {
            if (!encendedorCollider.gameObject.GetComponent<CandleController>().velaEncendida)
            {
                StartCoroutine(MyCoroutine(encendedorCollider));
            }
            else
            {
                Debug.Log("La vela ya esta encendida, tienes que esperar a que se apague :(");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Estoy en el area de " + other);
        if (other.name == "encendedor1")
        {
            encendedor = true;
            encendedorCollider = other;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "encendedor1")
        {
            encendedor = false;
        }
    }

    IEnumerator MyCoroutine(Collider other)
    {
        Debug.Log("Encendiendo... vela");
        player.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(5F);
        other.gameObject.GetComponent<CandleController>().encenderVela();
        player.GetComponent<PlayerController>().enabled = true;
    }

}

