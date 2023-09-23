using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelStartScreen : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject go;

    public void SetLevelTitle(string levelTitle)
    {
        title.text = levelTitle;
    }

    public void GoTime()
    {
        go.SetActive(true);
    }
}
