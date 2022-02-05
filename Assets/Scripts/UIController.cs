using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Main menu vars
    private bool splashActive = false;
    
    
    public GameObject fadeOut;
    public GameObject[] menuButtons;
    public GameObject logo;
    public GameObject pressAnyText;
    public GameObject buttonBG;

    //Pause menu vars





    //Prevent the controller from being destroyed. If there are more than 1, destroy the duplicates.
    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UIController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        menuButtons = GameObject.FindGameObjectsWithTag("UIButton");

        if (currentSceneName == "MainMenu"){
            //Setup the main menu variables
            StartCoroutine(WaitToProceed(2f));
        }
        else if(currentSceneName == "FirstLevel"){

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (splashActive && Input.anyKey){
            StartCoroutine("ActivateMainMenu");
            splashActive = false;
        }
    }

    private IEnumerator ActivateMainMenu(){
        Animator logoAnimator = logo.GetComponent<Animator>();
        Animator pressAnyAnimator = pressAnyText.GetComponent<Animator>();
        logoAnimator.SetBool("splashScreen", false);
        pressAnyAnimator.SetBool("splashScreen", false);

        yield return new WaitForSeconds(.5f);
        
        buttonBG.SetActive(true);

        yield return new WaitForSeconds(.1f);
        foreach(GameObject button in menuButtons){
            button.GetComponent<Image>().enabled = true;
            button.transform.GetChild(0).GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator WaitToProceed(float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        splashActive = true;
        pressAnyText.SetActive(true);
    }

    public void StartButton(){
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
    }

    public void HowToButton(){
        //stuff
    }

    public void CreditsButton(){
        //stuff
    }

    public void ExitButton(){
        StartCoroutine(LeaveGame());
    }

    private IEnumerator LeaveGame(){
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
