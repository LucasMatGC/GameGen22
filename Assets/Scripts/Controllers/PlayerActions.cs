using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;

    public float cameraMovementSpeed = 5f;

    string task = "";
    bool doingTask = false;
    int dontYouDare = 0;

    Collider encendedorCollider;
    Collider rezarCollider;
    Collider leerCollider;
    Collider reliquiaCollider;

    void Update()
    {

        if (Input.GetButtonDown("Action"))
        {
            switch (task)
            {
                case "lighter":
                    if (!encendedorCollider.gameObject.GetComponent<CandleController>().busy)
                    {
                        StartCoroutine(startAction(task, encendedorCollider));
                    }
                    //else
                    //{
                    //    Debug.Log("La vela ya esta encendida, tienes que esperar a que se apague :(");
                    //}
                    break;

                case "altar":
                    if (!rezarCollider.gameObject.GetComponent<PrayController>().busy)
                    {
                        StartCoroutine(startAction(task, rezarCollider));
                    }
                    //else
                    //{
                    //    Debug.Log("Ya hay alguien rezando, espera a que este libre");
                    //}
                    break;

                case "book":
                    if (!leerCollider.gameObject.GetComponent<TableController>().busy)
                    {
                        StartCoroutine(startAction(task, leerCollider));
                    }
                    //else
                    //{
                    //    Debug.Log("Ya hay alguien leyendo en este sitio, espera a que este libre o busca otro");
                    //}
                    break;

                case "relic":
                    if (!reliquiaCollider.gameObject.GetComponent<RelicController>().busy)
                    {
                        StartCoroutine(startAction(task, reliquiaCollider));
                    }
                    //else
                    //{
                    //    Debug.Log("Ya hay alguien leyendo en este sitio, espera a que este libre o busca otro");
                    //}
                    break;

                default:
                    StartCoroutine(startAction("sweep", null));
                    break;
            }           
            
        }
        //________________________________Zoom in camera__________________________________________
        if (doingTask)
        {
            //Rotation
            Quaternion newRotation = Quaternion.Euler(50, 45, 0);
            playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, newRotation, cameraMovementSpeed * Time.deltaTime);
            //Position
            Vector3 posicionPer = player.transform.position;
            Vector3 newPos = new Vector3(posicionPer.x - 1, posicionPer.y + 2.3f, posicionPer.z - 1);
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, newPos, cameraMovementSpeed * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!task.Equals(""))
        {
            dontYouDare++;
        }
        switch (other.gameObject.layer)
        {
            case 7:
                task = "lighter";
                encendedorCollider = other;
                break;
            case 9:
                task = "altar";
                rezarCollider = other;
                break;
            case 8:
                task = "book";
                leerCollider = other;
                break;
            case 10:
                task = "relic";
                reliquiaCollider = other;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (dontYouDare == 0)
        {
            int layerExit = other.gameObject.layer;
            if (7 <= layerExit && layerExit <= 10)
            {
                task = "";
            }
        }
        else
        {
            dontYouDare--;
        }       
    }

    IEnumerator startAction(string currentTask, Collider other)
    {
        doingTask = true;
        player.GetComponent<PlayerController>().enabled = false;
        playerCamera.GetComponent<CameraController>().enabled = false;
        float actionTime = 0f;
        switch (currentTask)
        {
            case "lighter":
                //Debug.Log("Encendiendo... vela");
                actionTime = 5f;
                other.gameObject.GetComponent<CandleController>().lightCandle(actionTime);
                break;
            case "altar":
                //Debug.Log("Rezando...");
                actionTime = 5f;        
                other.gameObject.GetComponent<PrayController>().pray(actionTime);
                break;
            case "book":
                //Debug.Log("Leyendo...");
                actionTime = 5f;
                other.gameObject.GetComponent<TableController>().read(actionTime);
                break;
            case "relic":
                //Debug.Log("Viendo la reliquia...");
                actionTime = 5f;
                other.gameObject.GetComponent<RelicController>().watch(actionTime);
                break;
            case "sweep":
                //Debug.Log("Barriendo...");
                actionTime = 5f;                
                break;
        }
        yield return new WaitForSeconds(actionTime);
        playerCamera.GetComponent<CameraController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        doingTask = false;
    }
          


}

