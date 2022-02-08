using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject eyeball;
    public GameObject playerstats;
    public AudioSource heartbeat;

    public float timerEndOfGame = 300.0f;

    public float maxTaskTime = 60f;

    public float timerForTask1;
    public float timerForTask2;
    public float timerForTask3;

    public int life = 3;
    private bool isGameActive;

    public static GameController instance;



        void Start()
    {
            instance = this;
            isGameActive = true;
            timerForTask1 = maxTaskTime;
            timerForTask2 = maxTaskTime;
            timerForTask3 = maxTaskTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (isGameActive)
        {

            timerEndOfGame -= Time.deltaTime;

            timerForTask1 -= Time.deltaTime;
            timerForTask2 -= Time.deltaTime;
            timerForTask3 -= Time.deltaTime;

            if (timerEndOfGame <= 0.0f)
            {
                ChooseMenu();
            }

            if ((timerForTask1 <= 0.0f || timerForTask2 <= 0.0f || timerForTask3 <= 0.0f))
            {
                Damage();
            }

            if (life <= 0)
            {
                Defeat();
            }

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

        isGameActive = false;

        SceneManager.LoadScene("EndScreen"/*, LoadSceneMode.Single*/);

    }

    void Damage()
    {
        life -= 1;

        if (timerForTask1 <= 0.0f)
            timerForTask1 = maxTaskTime;

        if (timerForTask2 <= 0.0f)
            timerForTask2 = maxTaskTime;

        if (timerForTask3 <= 0.0f)
            timerForTask3 = maxTaskTime;

        StartCoroutine(DamageAnimation());
    }

    void Defeat()
    {
        isGameActive = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerActions>().enabled = false;
        UIController.instance.DeathMenu();
        Debug.Log("YOU LOSE!");
    }

    IEnumerator DamageAnimation()
    {

        eyeball.SetActive(true);
        heartbeat.Play();
        yield return new WaitForSeconds(1f);
        eyeball.SetActive(false);

    }
}