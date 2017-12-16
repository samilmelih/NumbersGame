using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {


    public GameObject levelPickerPrefab;
    public Transform contentObject;
   

    public int countOfLevel;
    public float levelPickerWidth;
    public float spacing ;

    List<Level> levels;
	// Use this for initialization
	void Start () {

        levels = LevelGenerator.ReadLevels();
        countOfLevel = levels.Count;

        //we have to change content width up to level count
        levelPickerWidth = (levelPickerPrefab.transform as RectTransform).sizeDelta.x;
        spacing = contentObject.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
        (contentObject as RectTransform).sizeDelta = new Vector2((levelPickerWidth + spacing) * (countOfLevel), (contentObject as RectTransform).sizeDelta.y);

        AddLevels();

        
	}
	void AddLevels()
    {
        for (int i = 0; i < countOfLevel; i++)
        {
            GameObject levelPicker = Instantiate(levelPickerPrefab, contentObject);
            levelPicker.name = i.ToString();


            Transform backround = levelPicker.transform.Find("Background");

                              //set a sprite to background of this levelPicker
                              

            int k = 0;
            Transform levelTable = levelPicker.transform.Find("LevelTable");
            Transform table = levelTable.transform.Find("Table");
            for (int j = 0; j < table.childCount; j++)
            {
               
                    if (k < levels[i].design.Count && levels[i].design[k] == j + 1)
                    {
                        table.GetChild(j).GetComponent<CardSelection>().SelectCard();
                        k++;
                    }
                    else
                    {
                        table.GetChild(j).GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.5f);
                    }
        
            }
            
           


           
            

        }
    }
	
}
