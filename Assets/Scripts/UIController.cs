using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class UIController : MonoBehaviour
{
    //Main menu vars
    private bool splashActive = false;
    

    public GameObject logo;
    public GameObject pressAnyText;
    
    //Pause menu vars
    private bool pause = false;
    private GameObject player;
    private bool readyToPause = true;
    private Color[] colors= {new Color32(24,255,0,255), new Color32(0,169,255,255), new Color32(101,0,255,255), new Color32(255,29,0,255), new Color32(255,202,0,255)};

    public GameObject pauseCanvas;
    public GameObject postProcessingVolume;

    //Common vars
    string currentSceneName;
    GameObject myEventSystem;


    public GameObject buttonBG;
    public GameObject[] menuButtons;
    public GameObject fadeOut;


    //Lerping vars
    private float minWeight = 0f;
    private float maxWeight =  1.0f;

    static float t = 0.0f;
    private int blurLerp = 0;



    //Prevent the controller from being destroyed. If there are more than 1, destroy the duplicates.
    /*void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UIController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }*/


    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        menuButtons = GameObject.FindGameObjectsWithTag("UIButton");
        myEventSystem = GameObject.Find("EventSystem");

        if (currentSceneName == "MainMenu"){
            //Setup the main menu variables
            Time.timeScale = 1;
            StartCoroutine(WaitToProceed(2f));
        }
        else if(currentSceneName == "PauseMenuScene"){
            StartCoroutine(DeactivateFadeIn());
            player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (splashActive && Input.anyKey){
            StartCoroutine("ActivateMainMenu");
            splashActive = false;
        }
        
        if (currentSceneName == "PauseMenuScene" && readyToPause && Input.GetKeyDown(KeyCode.Escape)){
            PauseContinue();
        }

        if (blurLerp != 0){
            // animate the postprocessing effects...
            if(blurLerp == 1){
                postProcessingVolume.GetComponent<PostProcessVolume>().weight = Mathf.Lerp(minWeight, maxWeight, t);
            }
            else if (blurLerp == -1){
                postProcessingVolume.GetComponent<PostProcessVolume>().weight = Mathf.Lerp(maxWeight, minWeight, t);
            }
            // .. and increase the t interpolater
            t += 2.75f * Time.unscaledDeltaTime;

            // now check if the interpolator has reached 1.0
            // and swap maximum and minimum so game object moves
            // in the opposite direction.
            if (t > 1f || t < 0.0f)
            {
                blurLerp = 0;
                t = 0.0f;
            }
        }
    }

    private IEnumerator ActivateMainMenu(){
        Animator logoAnimator = logo.GetComponent<Animator>();
        Animator pressAnyAnimator = pressAnyText.GetComponent<Animator>();
        logoAnimator.SetBool("splashScreen", false);
        pressAnyAnimator.SetBool("splashScreen", false);

        yield return new WaitForSeconds(.5f);
        
        buttonBG.SetActive(true);
        StartCoroutine(ShowButtons());
    }

    private IEnumerator ShowButtons(){
        buttonBG.GetComponent<Animator>().SetBool("hide", false);

        yield return new WaitForSecondsRealtime(.1f);
        foreach(GameObject button in menuButtons){
            button.GetComponent<Image>().enabled = true;
            button.transform.GetChild(0).GetComponent<Text>().enabled = true;
            yield return new WaitForSecondsRealtime(.1f);
        }
        readyToPause = true;
    }

    private IEnumerator HideButtons(){
        buttonBG.GetComponent<Animator>().SetBool("hide", true);

        for (int i = menuButtons.Length - 1; i >= 0; i--){
            menuButtons[i].GetComponent<Image>().enabled = false;
            menuButtons[i].transform.GetChild(0).GetComponent<Text>().enabled = false;
            yield return new WaitForSecondsRealtime(.08f);
        }
        yield return new WaitForSecondsRealtime(0.15f);
        readyToPause = true;
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    private IEnumerator WaitToProceed(float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        splashActive = true;
        pressAnyText.SetActive(true);
    }

    public void StartButton(){
        StartCoroutine(ChangeScene("PauseMenuScene"));
    }

    public void HowToButton(){
        //stuff
    }

    public void CreditsButton(){
        //stuff
    }

    public void ExitButton(){
        StartCoroutine(ChangeScene("exit"));
    }

    public void BackToMainMenuButton(){
        StartCoroutine(ChangeScene("MainMenu"));
    }

    public void PauseContinue(){
        pause = !pause;
        player.GetComponent<PlayerController>().enabled = !pause;
        if(pause){
            Bloom bloom;
            postProcessingVolume.GetComponent<PostProcessVolume>().profile.TryGetSettings(out bloom);
            bloom.color.value = colors[Random.Range(0, colors.Length)];
            Time.timeScale = 0;
            blurLerp = 1;
            buttonBG.GetComponent<Image>().enabled = true;
            pauseCanvas.GetComponent<Canvas>().enabled = true;
            readyToPause = false;
            StartCoroutine(ShowButtons());
        } 
        else{
            myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
            readyToPause = false;
            StartCoroutine(HideButtons());
            blurLerp = -1;
            Time.timeScale = 1;
        } 
    }

    private IEnumerator ChangeScene(string sceneName){
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSecondsRealtime(1f);
        if (sceneName != "exit") SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        else Application.Quit();
    }

    public IEnumerator DeactivateFadeIn(){
        yield return new WaitForSeconds(1);
        fadeOut.SetActive(false);
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }
}
