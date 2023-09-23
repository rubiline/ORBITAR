using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Cutscene")]
public class Cutscene : ScriptableObject
{
    public string ID;
    public List<Sprite> Slideshow;
    public List<string> Text;
    public TimelineAsset Timing;
    public string SceneToLoad;
}
