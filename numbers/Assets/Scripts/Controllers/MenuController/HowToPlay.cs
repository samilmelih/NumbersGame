using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour {

    [Header("UnityStuffs")]
    public TextMeshProUGUI descriptionText;
    public GameObject howToPlayScreen;
    string[] scripts;
    public Button[] buttons;
    private void Start()
    {
        index = 0;
        int langIndex = PlayerPrefs.HasKey("lang") ? PlayerPrefs.GetInt("lang") : 0;

        scripts = StringLiterals.scripts[langIndex];
       
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.nextButton[langIndex];
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.exitButton[langIndex];
    }
    int index = 0;
    public void NextButton()
    {
       
        
        if (index == scripts.Length)
        {        
            HideHowToPlayScreen();
            return;
        }
         descriptionText.text =scripts[index];
        index++;
    }
    public void HideHowToPlayScreen()
    {
        index = 0;
        howToPlayScreen.SetActive(false);
        //burada oyunu başlatıyorum kaldığı yerden devam edecek
		FindObjectOfType<LevelUIController>().ToggleMenuAnim();
    }
   
}
