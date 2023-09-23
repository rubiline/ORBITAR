using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    public string textContent;
    public MenuBox parent;
    public float width;
    public float height;
    public TextMeshPro text;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(parent == null)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void SetText(string text)
    {
        textContent = text;
        DrawBox();
    }

    public void DrawBox()
    {
        sprite.size = new Vector2 (width, height);
        text.rectTransform.sizeDelta = new Vector2 (width - 0.5f, height - 0.5f);
        text.text = textContent;
    }


}
