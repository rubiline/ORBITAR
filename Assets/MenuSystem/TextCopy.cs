using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCopy : MonoBehaviour
{
    public TextMeshPro parent;
    public TextMeshPro text;

    void Awake()
    {
        parent = this.transform.parent.GetComponent<TextMeshPro>();
        text = this.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = parent.text;
    }
}
