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
        "After that you can have another chance by touching that button.But you have to watch an Advertisement video."+
            "If you skip ads you can look at cards 2 seconds. If you watch it all you will have 5 seconds",
        "You can use this button in every mode.",
        "There are 3 different modes. Classic, Don't Forget, No Mistake",
        "\t Classic \n"+
            "In this mode, there are too many variables for calculating stars. You are responsible for the passed time and your wrong tries.",
       "\t Don't Forget \n"+
            "In this mode, You have memorise cards because you are responsible for your wrong tries. Less mistake more stars.",
       "\t No Mistake \n"+
            "In this mode, you can not make mistakes. Game will over with your first missclick.",
        "Have fun! And if you like it rate us on Google Play..."

    };

    private void Start()
    {
        index = 0;
        
        
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
