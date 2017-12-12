using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownController : MonoBehaviour
{
    public static DropdownController Instance;

    [Header("Unity Stuffs")]
    public TMP_Dropdown LevelmodeDropdown;
    public TMP_Dropdown DifficultyDropdown;


    public LevelMode levelMode = LevelMode.NONE;
    public LevelDifficulty levelDifficulty = LevelDifficulty.NONE;

    void Start()
    {

        if (Instance != null)
            return;

        Instance = this;

        List<string> enumNameList = new List<string>();


        enumNameList.AddRange(Enum.GetNames(typeof(LevelMode)));
        LevelmodeDropdown.AddOptions(enumNameList);
        enumNameList.Clear();

        enumNameList.AddRange(Enum.GetNames(typeof(LevelDifficulty)));
        DifficultyDropdown.AddOptions(enumNameList);
        enumNameList.Clear();

    }


    public void LevelModeChooser(int index)
    {
        levelMode = (LevelMode)Enum.GetValues(typeof(LevelMode)).GetValue(index);
    }

    public void DifficultyChooser(int index)
    {
        levelDifficulty = (LevelDifficulty)Enum.GetValues(typeof(LevelDifficulty)).GetValue(index);
    }
}
