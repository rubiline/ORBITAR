using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public MenuBox focus;
    public GameObject cursor;
    public float cursorOffset;
    public int cursorIndex;

    void Awake()
    {
        //singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) OnMoveUp();
        if (Input.GetKeyDown(KeyCode.DownArrow)) OnMoveDown();
        if (Input.GetKeyDown(KeyCode.Z)) OnSelect();
        if (Input.GetKeyDown(KeyCode.X)) OnCancel();
    }

    public void OnMoveUp()
    {
        //don't do anything if we have no menu
        if (focus == null) return;
        //unhighlight old item
        focus.items[cursorIndex].UnHighlight();
        //set menu item index
        cursorIndex -= 1;
        if (cursorIndex < 0) cursorIndex = 0;
        //move cursor
        MoveCursor();
        //highlight new item
        focus.items[cursorIndex].Highlight();
        //TEST play sound
        AudioManager.Instance.PlaySFX("tick");
    }

    public void OnMoveDown()
    {
        //don't do anything if we have no menu
        if (focus == null) return;
        //unhighlight old item
        focus.items[cursorIndex].UnHighlight();
        //set menu item index
        cursorIndex += 1;
        if (cursorIndex >= focus.items.Count) cursorIndex = focus.items.Count - 1;
        //move cursor
        MoveCursor();
        //highlight new item
        focus.items[cursorIndex].Highlight();
        //TEST play sound
        AudioManager.Instance.PlaySFX("tick");
    }

    public void OnSelect()
    {
        if (focus == null) return;
        focus.items[cursorIndex].Select();
        //TEST play sound
        AudioManager.Instance.PlaySFX("select");
    }

    public void OnCancel()
    {
        if (focus == null) return;
        if (focus.parent == null) return;
        MenuBox former = focus;
        SetFocus(focus.parent);
        GameObject.Destroy(former.gameObject);
        //TEST play sound
        AudioManager.Instance.PlaySFX("cancel");
    }

    public void DestroyMenu()
    {
        GameObject.Destroy(focus.gameObject);
    }

    public void SetFocus(MenuBox target)
    {
        if(focus != null && focus != target)
        {
            focus.active = false;
            focus.DrawMenu();
        }
        focus = target;
        focus.active = true;
        cursorIndex = 0;
        focus.DrawMenu();
        MoveCursor();
        focus.items[cursorIndex].Highlight();
    }

    public void MoveCursor()
    {
        cursor.transform.position = focus.GetItemPos(cursorIndex) + new Vector2(cursorOffset, 0);
    }

    public MenuBox CreateMenu(GameObject menuPrefab, string title, List<MenuItem> items, float width, Vector2 position)
    {
        MenuBox newMenu = GameObject.Instantiate(menuPrefab).GetComponent<MenuBox>();
        newMenu.transform.parent = this.transform;
        newMenu.transform.localPosition = position;
        newMenu.title = title;
        newMenu.items = items;
        newMenu.width = width;
        SetFocus(newMenu);
        return newMenu;
    }

    public MenuBox CreateMenu(GameObject menuPrefab, string title, List<MenuItem> items, float width, Vector2 position, MenuBox parent)
    {
        MenuBox newMenu = GameObject.Instantiate(menuPrefab).GetComponent<MenuBox>();
        newMenu.transform.parent = this.transform;
        newMenu.transform.localPosition = position;
        newMenu.title = title;
        newMenu.items = items;
        newMenu.width = width;
        newMenu.parent = parent;
        newMenu.parentIndex = parent.parentIndex + 1;
        SetFocus(newMenu);
        return newMenu;
    }

    public MenuBox CreateMenuChild(GameObject menuPrefab, string title, List<MenuItem> items, float width, Vector2 position)
    {
        MenuBox newMenu = GameObject.Instantiate(menuPrefab).GetComponent<MenuBox>();
        newMenu.transform.parent = this.transform;
        newMenu.transform.localPosition = position;
        newMenu.title = title;
        newMenu.items = items;
        newMenu.width = width;
        newMenu.parent = focus;
        newMenu.parentIndex = focus.parentIndex + 1;
        SetFocus(newMenu);
        return newMenu;
    }

    public TextBox CreateTextBox(GameObject boxPrefab, string text, float width, float height, Vector2 position)
    {
        TextBox newBox = GameObject.Instantiate(boxPrefab).GetComponent<TextBox>();
        newBox.transform.parent = this.transform;
        newBox.transform.localPosition = position;
        newBox.width = width;
        newBox.height = height;
        newBox.parent = focus;
        newBox.textContent = text;
        newBox.DrawBox();
        return newBox;
    }

    public TextBox CreateTextBox(GameObject boxPrefab, string text, float width, float height, Vector2 position, MenuBox parent)
    {
        TextBox newBox = GameObject.Instantiate(boxPrefab).GetComponent<TextBox>();
        newBox.transform.parent = this.transform;
        newBox.transform.localPosition = position;
        newBox.width = width;
        newBox.height = height;
        newBox.parent = parent;
        newBox.textContent = text;
        newBox.DrawBox();
        return newBox;
    }

}
