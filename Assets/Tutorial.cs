using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<string> Dialogue = new List<string>();
    private int _index;
    public DialogueTextBoxUGUI Box;
    public bool IsDone = false;

    public bool SunShowing = false;
    public GameObject Sun;
    public GameObject Moon;

    private void Update()
    {
        if (!IsDone && Input.GetKeyDown(KeyCode.Z))
        {
            if (_index >= Dialogue.Count)
            {
                IsDone = true;

            } else
            {
                Advance();
            }
        }
    }

    public void Advance()
    {
        Box.SetText(Dialogue[_index]);
        _index++;
        AudioManager.Instance.PlaySFX("tick");

        if (SunShowing)
        {
            Sun.SetActive(true);
            Moon.SetActive(false);
        } else
        {
            Sun.SetActive(false);
            Moon.SetActive(true);
        }

        SunShowing = !SunShowing;
    }
}
