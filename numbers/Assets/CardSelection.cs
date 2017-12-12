using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [Header("Card Info")]
    public int index;
    public Color selectColor = Color.blue;
    public Color deselectColor = Color.white;
    [Header("UnityStuffs")]
    public GeneratorMaster generatorMaster;

    private bool cardSelected;
    // Use this for initialization
    void Start()
    {
        generatorMaster = GeneratorMaster.Instance;
    }


    public void OnCardClikced()
    {
        if (!cardSelected)
        {
            GetComponentInChildren<Image>().color = selectColor;
            cardSelected = true;
            generatorMaster.AddCardToList(index);
        }
        else
        {
            GetComponentInChildren<Image>().color = deselectColor;
            cardSelected = false;
            generatorMaster.RemoveCardFromList(index);
        }
    }
}
