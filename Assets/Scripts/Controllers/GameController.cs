using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public float timerEndOfGame = 300.0f;

    public float timerForTask1 = 60.0f;
    public float timerForTask2 = 60.0f;
    public float timerForTask3 = 60.0f;


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
        timerForTask1 = 60.0f;
    }
    public void ResetTimer2()
    {
        timerForTask2 = 60.0f;
    }
    public void ResetTimer3()
    {
        timerForTask3 = 60.0f;
    }

    void ChooseMenu()
    {
        Debug.Log("Eleccion de las tareas");
    }

    void Defeat()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerActions>().enabled = false;
        Debug.Log("YOU LOSE!");
    }
}
