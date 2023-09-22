using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector Director;
    public List<Cutscene> CutsceneList;
    public SpriteRenderer SlideshowHost;

    private int _index;
    private Cutscene _currentCutscene;
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        PlayCutscene(GameManager.Instance.CurrentCutscene);
    }

    void PlayCutscene(string cutsceneName)
    {
        Cutscene c = CutsceneList.Find(a => a.ID == cutsceneName);
        if (c != null)
        {
            _currentCutscene = Instantiate(c);
            Director.playableAsset = _currentCutscene.Timing;
            Director.Play();
        }
    }

    void Advance()
    {
        _index++;
        SlideshowHost.sprite = _currentCutscene.Slideshow[_index];
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0, 1));
    }

    private IEnumerator Fade(float from, float to, float time)
    {
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            GameManager.Instance.GBMaterial.SetFloat("Fade", Mathf.Lerp(from, to, time - currentTime));
            yield return null;
        }
    }

}
