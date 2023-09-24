using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) OnStart();
    }

    void OnStart()
    {
        menuManager.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}
