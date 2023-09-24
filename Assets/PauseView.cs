using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseView : MonoBehaviour
{
    public TextMeshProUGUI GoldTime;
    public TextMeshProUGUI SilverTime;
    public TextMeshProUGUI BronzeTime;
    public TextMeshProUGUI YourTime;

    // Start is called before the first frame update
    void Start()
    {
        Parse();
    }

    void Parse()
    {
        string stage = GameManager.Instance.currentLoadedScene;

        GoldTime.text = LevelManager.Instance.goldTime.ToString(@"m\:ss\.fff");
        SilverTime.text = LevelManager.Instance.silverTime.ToString(@"m\:ss\.fff");
        BronzeTime.text = LevelManager.Instance.bronzeTime.ToString(@"m\:ss\.fff");

        string time = PlayerPrefs.GetString(stage + "_time", "");
        if (time != "")
        {
            YourTime.text = time;
        }
        else
        {
            YourTime.text = "-";
        }

    }
}