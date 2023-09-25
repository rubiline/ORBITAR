using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cred
    : MonoBehaviour
{

    public Transform Credits;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("SELECT_NOVA");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)) {
            LoadTitle();
        }

        if (Credits.position.y > 26)
        {
            LoadTitle();
        }
    }

    void LoadTitle()
    {
        GameManager.Instance.LoadScene("Title");
        Destroy(this.gameObject);
    }
}
