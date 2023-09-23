using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    public GameObject menuPrefab;
    public GameObject boxPrefab;
    public List<string> outputTest;

    // Start is called before the first frame update
    void Start()
    {
        Action itemMenu = () =>
        {
            TextBox descriptions = MenuManager.Instance.CreateTextBox(boxPrefab, "", 4, 2, new Vector2(2, 1));
            List<MenuItem> testItems = new List<MenuItem>();
            foreach (string item in outputTest)
            {
                testItems.Add(new MenuItem(item, () => Debug.Log("you have selected " + item), () => descriptions.SetText("this is option " + item)));
            }
            MenuManager.Instance.CreateMenuChild(menuPrefab, "", testItems, 3, new Vector2(-3, -2));
            descriptions.parent = MenuManager.Instance.focus;
        };
        Action titleChange1 = () => 
        { 
            MenuManager.Instance.focus.title = "secret!"; 
            MenuManager.Instance.focus.DrawMenu();
            Debug.Log("hello world");
        };
        Action titleChange2 = () => 
        { 
            MenuManager.Instance.focus.parent.title = "secret 2!"; 
            MenuManager.Instance.focus.parent.DrawMenu(); 
        };
        Action testAction2 = () => MenuManager.Instance.CreateMenuChild(menuPrefab, "Sub Menu 2", new List<MenuItem> { new MenuItem("test 1", null), new MenuItem("item select", itemMenu), new MenuItem("test 3", titleChange2), new MenuItem("test 4", titleChange1) }, 3, new Vector2(-2,2));
        Action testAction = () => MenuManager.Instance.CreateMenuChild(menuPrefab, "Sub Menu", new List<MenuItem> { new MenuItem("test 1", null), new MenuItem("test 2", testAction2) }, 3, new Vector2(3, -1));
        MenuManager.Instance.CreateMenu(menuPrefab, "Test Menu", new List<MenuItem> { new MenuItem("test 1", testAction) }, 3, new Vector2(0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
