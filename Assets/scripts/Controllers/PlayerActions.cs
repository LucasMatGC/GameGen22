using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject player;

    bool encendedor = false;
    Collider encendedorCollider;

    bool rezar = false;
    Collider rezarCollider;

    void Update()
    {
        if(encendedor == true && Input.GetButtonDown("Action"))
        {
            if (!encendedorCollider.gameObject.GetComponent<CandleController>().velaEncendida)
            {
                StartCoroutine(encendiendoVela(encendedorCollider));
            }
            else
            {
                Debug.Log("La vela ya esta encendida, tienes que esperar a que se apague :(");
            }
        }
        else if (rezar == true && Input.GetButtonDown("Action"))
        {
            if (!rezarCollider.gameObject.GetComponent<PrayController>().ocupado)
            {
                StartCoroutine(rezando(rezarCollider));
            }
            else
            {
                Debug.Log("Ya hay alguien rezando, espera a que este libre");
            }
        }
        else if (Input.GetButtonDown("Action"))
        {
            StartCoroutine(barrer());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Lighter1")
        {
            encendedor = true;
            encendedorCollider = other;
        }
        else if (other.name == "PrayingArea")
        {
            rezar = true;
            rezarCollider = other;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Lighter1")
        {
            encendedor = false;
        }
        else if (other.name == "PrayingArea")
        {
            rezar = false;
        }
    }

    IEnumerator encendiendoVela(Collider other)
    {
        Debug.Log("Encendiendo... vela");
        float tiempoDeEncendido = 5f;
        player.GetComponent<PlayerController>().enabled = false;
        other.gameObject.GetComponent<CandleController>().encenderVela(tiempoDeEncendido);
        yield return new WaitForSeconds(tiempoDeEncendido); 
        player.GetComponent<PlayerController>().enabled = true;
    }
    IEnumerator rezando(Collider other)
    {
        Debug.Log("Rezando...");
        float tiempoRezando = 5f;
        player.GetComponent<PlayerController>().enabled = false;
        other.gameObject.GetComponent<PrayController>().rezar(tiempoRezando);
        yield return new WaitForSeconds(tiempoRezando);
        player.GetComponent<PlayerController>().enabled = true;
    }
    IEnumerator barrer()
    {
        Debug.Log("Barriendo...");
        float tiempoBarriendo = 5f;
        player.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(tiempoBarriendo);
        player.GetComponent<PlayerController>().enabled = true;
    }

}

