using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button clicksCatcherButton;
    [SerializeField] private Button generateFieldButton;
    [SerializeField] private Button generateWallsButton;
    [SerializeField] private TMP_InputField fieldWidthInput;
    [SerializeField] private TMP_InputField fieldHeightInput;
    [SerializeField] private LabyrinthController labyrinthController;

    private void Awake()
    {
        clicksCatcherButton.onClick.AddListener(ClicksCatcherOnClick);
        generateFieldButton.onClick.AddListener(GenerateFieldOnClick);
        generateWallsButton.onClick.AddListener(GenerateWallsOnClick);

        fieldWidthInput.onValueChanged.AddListener(OnWidthValueChanged);
        fieldHeightInput.onValueChanged.AddListener(OnHeightValueChanged);
    }

    private void ClicksCatcherOnClick()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        clicksCatcherButton.gameObject.SetActive(true);
        fieldWidthInput.text = labyrinthController.LabyrinthWidth.ToString();
        fieldHeightInput.text = labyrinthController.LabyrinthHeight.ToString();
    }

    private void OnDisable()
    {
        clicksCatcherButton.gameObject.SetActive(false);
    }

    private void OnWidthValueChanged(string value)
    {
        int newValue;
        if (Int32.TryParse(value, out newValue))
            labyrinthController.LabyrinthWidth = newValue;
    }

    private void OnHeightValueChanged(string value)
    {
        int newValue;
        if (Int32.TryParse(value, out newValue))
            labyrinthController.LabyrinthHeight = newValue;
    }
    
    private void GenerateFieldOnClick()
    {
        labyrinthController.GenerateField();
    }

    private void GenerateWallsOnClick()
    {
        
    }
}
