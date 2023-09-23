using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string LevelSelect = "";

    void Start()
    {
        if (LevelSelect != "")
        {
            GameManager.Instance.LoadScene(LevelSelect);
            Destroy(gameObject);
        } else { 
            GameManager.Instance.LoadScene("Cutscene");
            Destroy(gameObject);
        }
    }
}
