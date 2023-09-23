using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MenuItem
{
    public string name;
    public Action selected;
    public Action highlighted;
    public Action unhighlighted;

    public MenuItem(string name, Action selected)
    {
        this.name = name;
        this.selected = selected;
    }

    public MenuItem(string name, Action selected, Action highlighted) : this(name, selected)
    {
        this.highlighted = highlighted;
    }


    public MenuItem(string name, Action selected, Action highlighted, Action unhighlighted) : this(name, selected, highlighted)
    {
        this.unhighlighted = unhighlighted;
    }

    public void Select()
    {
        selected?.Invoke();
    }

    public void Highlight()
    {
        highlighted?.Invoke();
    }

    public void UnHighlight()
    {
        unhighlighted?.Invoke();
    }
}
