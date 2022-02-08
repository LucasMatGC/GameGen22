using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject[] optionButtons;
    public GameObject[] answerButtons;
    public GameObject confirmCanvas;
    public GameObject flairTextGO;
    public GameObject banner;
    public GameObject[] confirmButtons;
    public GameObject fadeOut;
    public GameObject finalCanvas;
    public GameObject scoreText;
    public GameObject finalText;

    private string[] unorderedAnswers = {"Sweep", "Light a candle", "Pray", "Touch the altar", "Speak with cultmaster", "Read a book", "Poison the potion"};
    private PlayerStats playerStats;
    private List<string> correctAnswers;
    private string tempStr;
    private List<string> pickedAnswers = new List<string>();
    private string flairTextWaitForAnswer = "Those 3 tasks were:";
    private int finalScore = 0;


    private void Start(){
        //GET CORRECT ANSWERS LIST HERE
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        correctAnswers = new List<string>(playerStats.MandatoryTasks());

        correctAnswers.Add("Sweep");
        correctAnswers.Add("Pray");
        correctAnswers.Add("Light a candle");

        Shuffle();
        for (int i = 0; i < optionButtons.Length; i++){
            optionButtons[i].transform.GetChild(0).GetComponent<Text>().text = unorderedAnswers[i];
        }
        StartCoroutine(waitForFlairText());
    }

    private IEnumerator waitForFlairText(){
        yield return new WaitForSeconds(18f);
        for (int i = 0; i < optionButtons.Length; i++){
            optionButtons[i].SetActive(true);
        }
    }

    public void PickAnswer(Text answer){
        if (pickedAnswers.Count < 3){
            pickedAnswers.Add(answer.text);
            answerButtons[pickedAnswers.Count-1].transform.GetChild(0).GetComponent<Text>().text = answer.text;
            answerButtons[pickedAnswers.Count-1].SetActive(true);

            if (pickedAnswers.Count >= 3){
                StartCoroutine(DisplayBanner());
            }
        }
    }

    public void HideAnswer(int index){
        if (pickedAnswers.Count < 3){
            optionButtons[index].SetActive(false);
        }
    }

    public void RemoveAnswer(int index){
        Cancel();
        pickedAnswers.RemoveAt(index);

        for (int i = 0; i < optionButtons.Length; i++){
            if (optionButtons[i].transform.GetChild(0).GetComponent<Text>().text == answerButtons[index].transform.GetChild(0).GetComponent<Text>().text){
                optionButtons[i].SetActive(true);
            }
        }

        for (int i = 0; i < answerButtons.Length; i++){
            answerButtons[i].SetActive(false);
        }

        for (int i = 0; i < pickedAnswers.Count; i++){
            answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = pickedAnswers[i];
            answerButtons[i].SetActive(true);
        }

    }

    public void Shuffle() {
    for (int i = 0; i < unorderedAnswers.Length; i++) {
        int rnd = Random.Range(0, unorderedAnswers.Length);
        tempStr = unorderedAnswers[rnd];
        unorderedAnswers[rnd] = unorderedAnswers[i];
        unorderedAnswers[i] = tempStr;
        }
    }

    public void Cancel(){
        StartCoroutine(HideBanner());
    }

     private IEnumerator DisplayBanner(){
        flairTextGO.SetActive(false);
        confirmCanvas.SetActive(true);
        banner.GetComponent<Animator>().SetBool("hide", false);
        yield return new WaitForSeconds(0);
        foreach(GameObject confirmButton in confirmButtons){
            confirmButton.SetActive(true);
        }
        
    }

    private IEnumerator HideBanner(){
        banner.GetComponent<Animator>().SetBool("hide", true);
        foreach(GameObject confirmButton in confirmButtons){
            confirmButton.SetActive(false);
        }
        yield return new WaitForSeconds(0);
        confirmCanvas.SetActive(false);
        flairTextGO.SetActive(true);
    }

    public void ConfirmButton(){
        fadeOut.SetActive(true);

        StartCoroutine(FinalMessage());
        
        for (int i = 0; i < pickedAnswers.Count; i++){
            string shortAnswer;
            switch (pickedAnswers[i]){
                case "Sweep":
                    shortAnswer = "sweep";
                    break;
                case "Light a candle":
                    shortAnswer = "lighter";
                    break;
                case "Pray":
                    shortAnswer = "pray";
                    break;
                case "Touch the altar":
                    shortAnswer = "watch";
                    break;
                case "Speak with cultmaster":
                    shortAnswer = "speak";
                    break;
                case "Read a book":
                    shortAnswer = "read";
                    break;
                default:
                    shortAnswer = "poison";
                    break;
            }
            if (correctAnswers.Contains(shortAnswer)){
                finalScore++;
            }
        }
    }

    private IEnumerator FinalMessage(){
        fadeOut.GetComponent<Animator>().SetBool("fadeOut", true);

        yield return new WaitForSeconds(1f);
    
        finalCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        confirmCanvas.SetActive(false);

        scoreText.GetComponent<Text>().text = "You got " + finalScore.ToString() + " rules right";
        string finalMessage = "";

        switch(finalScore){
            case 0:
                finalMessage = "You couldn't remember a single rule. How you survived that long in the cult without them noticing is a mystery, even to you.;However, your hazy memory and the wrong data you've spread around will keep the cult protected.;Waiting... For its new victim...";
                break;
            case 3:
                finalMessage = "You stayed within the cult long enough to gather all the vital data you needed.;Your report to the local newspapers helped the authorities identify and shut the cult down.;Good job!";
                break;
            default:
                finalMessage = "You barely stayed long enough to gather what data you could find and leave the cult unscathed.;Your vague report was not precise enough to help the authorities fight the cult, but at least you're sure you've helped some poor sod.;You can't seem to stop feeling like you're being watched, though...";
                break;
        }

        finalText.GetComponent<TextWriter>().m_text = finalMessage;

        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame(){
        yield return new WaitForSeconds(30f);
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        Destroy(GameObject.Find("PlayerStats"));
        SceneManager.LoadScene("MainMenu"/*, LoadSceneMode.Single*/);
    }
}
