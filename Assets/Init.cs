using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string DebugLevel = "";

    void Start()
    {
        if (DebugLevel != "")
        {
            GameManager.Instance.LoadScene(DebugLevel);
            Destroy(gameObject);
        } else { 
            GameManager.Instance.LoadScene("Cutscene");
            Destroy(gameObject);
        }
    }
}
