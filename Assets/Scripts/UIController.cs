using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button clicksCatcherButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private MainMenuController mainMenuController;

    private void Awake()
    {
        clicksCatcherButton.onClick.AddListener(ClicksCatcherOnClick);
        mainMenuButton.onClick.AddListener(MainMenuOnClick);
    }

    private void ClicksCatcherOnClick()
    {
        throw new NotImplementedException();
    }

    private void MainMenuOnClick()
    {
        throw new NotImplementedException();
    }
}
