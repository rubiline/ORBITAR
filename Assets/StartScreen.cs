using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("THEME_OF_ORBIT");
    }
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
