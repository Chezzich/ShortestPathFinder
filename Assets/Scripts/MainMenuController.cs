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
    [SerializeField] private Button findPathButton;
    [SerializeField] private TMP_InputField fieldWidthInput;
    [SerializeField] private TMP_InputField fieldHeightInput;
    [SerializeField] private LabyrinthController labyrinthController;
    [SerializeField] private Toggle brushesToggle;
    [SerializeField] private Toggle animsToggle;
    [SerializeField] private TMP_Dropdown brushesDropdown;

    private void Awake()
    {
        clicksCatcherButton.onClick.AddListener(ClicksCatcherOnClick);
        generateFieldButton.onClick.AddListener(GenerateFieldOnClick);
        generateWallsButton.onClick.AddListener(GenerateWallsOnClick);
        findPathButton.onClick.AddListener(FindPathOnClick);

        fieldWidthInput.onValueChanged.AddListener(OnWidthValueChanged);
        fieldHeightInput.onValueChanged.AddListener(OnHeightValueChanged);

        animsToggle.onValueChanged.AddListener(OnAnimsToggleValueChanged);
        brushesToggle.onValueChanged.AddListener(OnBrushesToggleValueChanged);
        brushesDropdown.onValueChanged.AddListener(OnBrushesDropdownValueChanged);
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
        labyrinthController.GenerateWalls();
    }

    private void OnBrushesToggleValueChanged(bool value)
    {
        LabyrinthController.IsActiveBrushes = value;
        brushesDropdown.gameObject.SetActive(value);
    }

    private void OnBrushesDropdownValueChanged(int value)
    {
        LabyrinthController.ActiveBrushType = (FieldCellInfo.FieldCellType)value;
    }

    private void FindPathOnClick()
    {
        StartCoroutine(labyrinthController.FindPath());
    }

    private void OnAnimsToggleValueChanged(bool value)
    {
        LabyrinthController.IsActiveAnimations = value;
    }
}
