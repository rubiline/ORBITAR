using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipButton : MonoBehaviour
{
    [SerializeField] PlayableDirector d;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) Skip();
    }

    void Skip()
    {
        d.time = d.duration;
    }
}
