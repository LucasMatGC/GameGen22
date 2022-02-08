using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public GameObject[] optionButtons;
    public GameObject[] answerButtons;
    public GameObject confirmCanvas;
    public GameObject flairTextGO;
    public GameObject banner;
    public GameObject[] confirmButtons;
    public GameObject fadeOut;
    public GameObject finalCanvas;
    public GameObject finalText;
    
    private string[] unorderedAnswers = {"Sweep", "Light a candle", "Pray", "Touch the altar", "Speak with cultmaster", "Read a book", "Improve potion"};
    private List<string> correctAnswers = new List<string>();
    private string tempStr;
    private List<string> pickedAnswers = new List<string>();
    private string flairTextWaitForAnswer = "Those 3 tasks were:";
    private int finalScore = 0;


    private void Start(){
        //GET CORRECT ANSWERS LIST HERE
        correctAnswers.Add("Sweep");
        correctAnswers.Add("Pray");
        correctAnswers.Add("Light a candle");

        Shuffle();
        for (int i = 0; i < optionButtons.Length; i++){
            optionButtons[i].transform.GetChild(0).GetComponent<Text>().text = unorderedAnswers[i];
        }
        //StartCoroutine(waitForFlairText());
    }

    private IEnumerator waitForFlairText(){
        yield return new WaitForSeconds(25f);
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
            if (correctAnswers.Contains(pickedAnswers[i])){
                finalScore++;
            }
        }
    }

    private IEnumerator FinalMessage(){
        yield return new WaitForSeconds(1f);

        finalCanvas.SetActive(true);

        finalText.GetComponent<Text>().text = finalScore.ToString();
    }
}
