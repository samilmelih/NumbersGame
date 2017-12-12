using UnityEngine;

public class LevelGeneratorButtons : MonoBehaviour
{

    [Header("Unity Stuffs")]
    public GameObject levelInfoScreen;
    public GameObject buttons;

    public void LevelInfoAccept()
    {
        //set all data
        GeneratorMaster.Instance.GenerateMap();
        //hide screen
        LevelInfoScreenShowHide();


    }

    public void LevelInfoReject()
    {
        //there is no change on map 
        LevelInfoScreenShowHide();
    }

    public void ResetButton()
    {
        GeneratorMaster.Instance.DeselectCards();
    }

    public void LevelInfoScreenShowHide()
    {
        levelInfoScreen.SetActive(!levelInfoScreen.activeSelf);
        buttons.SetActive(!buttons.activeSelf);
    }

    public void CreateLevel()
    {
        GeneratorMaster.Instance.CreateLevel();
    }
}
