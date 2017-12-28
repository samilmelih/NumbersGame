using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HowToPlay : MonoBehaviour {

    [Header("UnityStuffs")]
    public TextMeshProUGUI descriptionText;
    public GameObject howToPlayScreen;
    string[] scripts =
    {
        "Hello!\n"+
        "Welcome to follow the numbers. This instruction will teach you what to do in gameplay.",
        "It is a simple game to play. All you have to do is following the numbers.",
        "Okay! I am joking but it is right."+
        "In every card there is a number.",
        "You have to open them in the correct order.",
        "If you don't forget what you opened before, you may have 3 stars.",
        "If you would like to see the cards, you have one chance in every level! Touch the number you see at top! Yeah the big one...",
        "After that you can have another chance by touching that button.(just touch it)",
        "Have fun! And if you like it rate us on Google Play..."

    };

    private void Start()
    {
        index = 0;
        descriptionText.text = scripts[0];
    }
    int index = 0;
    public void NextButton()
    {
       
        index++;
        if (index == scripts.Length)
        {
            index = 0;
            howToPlayScreen.SetActive(false);
            //burada oyunu başlatıyorum kaldığı yerden devam edecek
            FindObjectOfType<LevelUIController>().CloseMenuAnim();
            return;
        }
         descriptionText.text =scripts[index];
    }
}
