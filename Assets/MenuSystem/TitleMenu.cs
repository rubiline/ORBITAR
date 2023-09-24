using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public GameObject menuPrefab;
    public GameObject boxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Action StartGame = () =>
        {
            GameManager.Instance._levelSelect = false;
            GameManager.Instance.LoadLevel("Stage 1-1");
        };

        Action ContinueGame = () =>
        {
            GameManager.Instance._levelSelect = false;
            GameManager.Instance.LoadLevel(PlayerPrefs.GetString("continue"));
        };

        Action LevelSelect = () =>
        {
            GameManager.Instance.LoadScene("LevelSelect");
        };

        Action Quit = () =>
        {
            Application.Quit();
        };

        Action SwapPalette = () =>
        {
            GameManager.Instance.SwapPalette();
        };


        Action OpenOptions = () =>
        {
            List<MenuItem> list = new List<MenuItem>()
            {
                new MenuItem("Palette", SwapPalette)
            };

            MenuManager.Instance.CreateMenuChild(menuPrefab, "", list, 4, new Vector2(1.5f,0));
        };

        List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem("NEW GAME", StartGame)
        };

        string cont = PlayerPrefs.GetString("continue", "");
        if (cont != "")
        {
            items.Add(new MenuItem("CONTINUE", ContinueGame));
        }
        items.Add(new MenuItem("LEVEL SELECT", LevelSelect));
        //TODO: Options
        items.Add(new MenuItem("OPTIONS", OpenOptions));
        items.Add(new MenuItem("QUIT", Quit));

        MenuManager.Instance.CreateMenu(menuPrefab, "", items, 5, new Vector2(0, 0));
    }
}
