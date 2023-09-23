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
            GameManager.Instance.Level = "Stage 1-1";
        };

        List<MenuItem> items = new List<MenuItem>()
        {
            new MenuItem("START", StartGame),
            new MenuItem("OPTIONS", null),
            new MenuItem("QUIT", null)
        };


        MenuManager.Instance.CreateMenu(menuPrefab, "", items, 5, new Vector2(0, 0));
    }
}
