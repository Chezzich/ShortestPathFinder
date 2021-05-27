using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private MainMenuController mainMenuController;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(MainMenuOnClick);
    }

    private void MainMenuOnClick()
    {
        mainMenuController.gameObject.SetActive(!mainMenuController.gameObject.activeSelf);
    }
}
