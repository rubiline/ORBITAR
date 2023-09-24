using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Cutscene")]
public class Cutscene : ScriptableObject
{
    public string ID;
    public List<Sprite> Slideshow;
    public List<Dialogue> Text;
    public List<CutsceneAnimation> Animations;
    public TimelineAsset Timing;
    public string SceneToLoad;
    public bool ShowBox1;
    public bool ShowBox2;
    public bool ShowBox3;
}

[Serializable]
public class Dialogue
{
    public string Text;
    public int Box;
}

[Serializable]
public class CutsceneAnimation
{
    public GameObject Prefab;
    public float Time;
    public Vector3 Location;
}
