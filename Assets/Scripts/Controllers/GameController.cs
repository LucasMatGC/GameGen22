using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public static GameController instance;

    public float timerEndOfGame = 300.0f;

    public float maxTaskTime = 60.0f;

    public float timerForTask1;
    public float timerForTask2;
    public float timerForTask3;

    private void Start()
    {

        instance = this;

        timerForTask1 = maxTaskTime;
        timerForTask2 = maxTaskTime;
        timerForTask3 = maxTaskTime;

    }


    // Update is called once per frame
    void Update()
    {
        timerEndOfGame -= Time.deltaTime;

        timerForTask1 -= Time.deltaTime;
        timerForTask2 -= Time.deltaTime;
        timerForTask3 -= Time.deltaTime;

        if (timerEndOfGame <= 0.0f)
        {
            ChooseMenu();
        }

        if (timerForTask1 <= 0.0f)
        {
            Defeat();
        }
        if (timerForTask2 <= 0.0f)
        {
            Defeat();
        }
        if (timerForTask3 <= 0.0f)
        {
            Defeat();
        }
    }

    public void ResetTimer1()
    {
        timerForTask1 = maxTaskTime;
    }
    public void ResetTimer2()
    {
        timerForTask2 = maxTaskTime;
    }
    public void ResetTimer3()
    {
        timerForTask3 = maxTaskTime;
    }

    void ChooseMenu()
    {
        Debug.Log("Eleccion de las tareas");
    }

    void Defeat()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerActions>().enabled = false;
        player.transform.rotation = new Quaternion(-45f, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
        Debug.Log("YOU LOSE!");
    }
}
