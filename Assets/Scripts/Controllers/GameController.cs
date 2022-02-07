using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public float timerEndOfGame = 300.0f;

    public float timerForTask1;
    public float timerForTask2;
    public float timerForTask3;

    public int life = 3;


    public static GameController instance;



        void Start()
    {
            instance = this;
            timerForTask1 = 60f;
        timerForTask2 = 60f;
        timerForTask3 = 60f;
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

        if (timerForTask1 <= 0.0f || timerForTask2 <= 0.0f || timerForTask3 <= 0.0f)
        {
            Damage();
        }
        if (life <= 0)
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

    void Damage()
    {
        life -= 1;
        timerForTask1 = 60f;
        timerForTask2 = 60f;
        timerForTask3 = 60f;
        Debug.Log("OUCH!");
    }

    void Defeat()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerActions>().enabled = false;
        Debug.Log("YOU LOSE!");
    }
}