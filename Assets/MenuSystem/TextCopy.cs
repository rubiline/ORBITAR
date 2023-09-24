using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCopy : MonoBehaviour
{
    public TextMeshProUGUI parent;
    public TextMeshProUGUI text;

    void Awake()
    {
        parent = this.transform.parent.GetComponent<TextMeshProUGUI>();
        text = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = parent.text;
    }
}
