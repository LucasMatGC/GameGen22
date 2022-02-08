using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject player;
    public GameObject broom;
    public GameObject playerCamera;
    public GameObject actionMaster;

    public float cameraMovementSpeed = 5f;

    string[] requiredActions = { "", "", "" };
    string task = "";
    bool doingTask = false;
    int dontYouDare = 0;

    Collider lighterCollider;
    Collider prayCollider;
    Collider readCollider;
    Collider relicCollider;
    Collider poisonCollider;
    Collider preacherCollider;



    void Start()
    {
        requiredActions = actionMaster.gameObject.GetComponent<ActionsMaster>().GiveMeActions();

    }

    void Update()
    {

        if (Input.GetButtonDown("Action"))
        {
            switch (task)
            {
                case "lighter":
                    if (!lighterCollider.gameObject.GetComponent<CandleController>().busy)
                    {
                        StartCoroutine(startAction(task, lighterCollider));
                    }                    
                    break;

                case "pray":
                    if (!prayCollider.gameObject.GetComponent<PrayController>().busy)
                    {
                        StartCoroutine(startAction(task, prayCollider));
                    }                    
                    break;

                case "read":
                    if (!readCollider.gameObject.GetComponent<TableController>().busy)
                    {
                        StartCoroutine(startAction(task, readCollider));
                    }                    
                    break;

                case "watch":
                    if (!relicCollider.gameObject.GetComponent<RelicController>().busy)
                    {
                        StartCoroutine(startAction(task, relicCollider));
                    }                    
                    break;

                case "poison":
                    if (!poisonCollider.gameObject.GetComponent<PoisonController>().busy)
                    {
                        StartCoroutine(startAction(task, poisonCollider));
                    }
                    break;

                case "speak":
                    if (!preacherCollider.gameObject.GetComponent<PreacherController>().busy)
                    {
                        StartCoroutine(startAction(task, preacherCollider));
                    }
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
                lighterCollider = other;
                break;
            case 9:
                task = "pray";
                prayCollider = other;
                break;
            case 8:
                task = "read";
                readCollider = other;
                break;
            case 10:
                task = "watch";
                relicCollider = other;
                break;
            case 11:
                task = "speak";
                preacherCollider = other;
                break;
            case 12:
                task = "poison";
                poisonCollider = other;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (dontYouDare == 0)
        {
            int layerExit = other.gameObject.layer;
            if (7 <= layerExit && layerExit <= 12)
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
        if (currentTask.Equals(requiredActions[0]))
        {
            Debug.Log("Reseteo tarea 1");
            GameController.instance.ResetTimer1();
        }
        else if(currentTask.Equals(requiredActions[1]))
        {
            Debug.Log("Reseteo tarea 2");
            GameController.instance.ResetTimer2();
        }
        else if (currentTask.Equals(requiredActions[2]))
        {
            Debug.Log("Reseteo tarea 3");
            GameController.instance.ResetTimer3();
        }
        
        float actionTime = 0f;
        switch (currentTask)
        {
            case "lighter":
                //Debug.Log("Encendiendo... vela");
                actionTime = 5f;
                other.gameObject.GetComponent<CandleController>().lightCandle(actionTime);
                break;
            case "pray":
                //Debug.Log("Rezando...");
                actionTime = 5f;        
                other.gameObject.GetComponent<PrayController>().pray(actionTime);
                break;
            case "read":
                //Debug.Log("Leyendo...");
                actionTime = 5f;
                other.gameObject.GetComponent<TableController>().read(actionTime);
                break;
            case "watch":
                //Debug.Log("Viendo la reliquia...");
                actionTime = 5f;
                other.gameObject.GetComponent<RelicController>().watch(actionTime);
                break;
            case "speak":
                //Debug.Log("Preaching...");
                actionTime = 5f;
                other.gameObject.GetComponent<PreacherController>().Speak(actionTime);
                break;
            case "poison":
                //Debug.Log("Envenenando...");
                actionTime = 5f;
                other.gameObject.GetComponent<PoisonController>().Poison(actionTime);
                break;
            case "sweep":
                //Debug.Log("Barriendo...");
                broom.SetActive(doingTask);
                actionTime = 5f;                
                break;
        }
        yield return new WaitForSeconds(actionTime);
        playerCamera.GetComponent<CameraController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        doingTask = false;
        broom.SetActive(doingTask);
    }   



}

