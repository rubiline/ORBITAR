using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadScene("Cutscene");
        Destroy(gameObject);
    }
}
