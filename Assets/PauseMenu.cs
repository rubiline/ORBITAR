using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPrefab;
    public GameObject boxPrefab;

    void Awake()
    {
        Action QuitLevel = () =>
        {
            GameManager.Instance.PauseControl.LevelManager.QuitLevel();
        };

        Action Return = () =>
        {
            GameManager.Instance.PauseControl.ToGameplay();
        };

        List<MenuItem> items = new List<MenuItem>()
        {
            new MenuItem("CONTINUE", Return),
            new MenuItem("QUIT", QuitLevel),
        };

        MenuManager.Instance.CreateMenu(menuPrefab, "", items, 4, new Vector2(0, 0));
    }
}
