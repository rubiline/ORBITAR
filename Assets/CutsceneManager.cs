using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector Director;
    public List<Cutscene> CutsceneList;
    public SpriteRenderer SlideshowHost;

    public List<DialogueTextBox> DialogueBoxList;

    public GameObject WaitPrompt;

    private int _imageIndex;
    private int _textIndex;
    private int _animationIndex;
    private Cutscene _currentCutscene;
    private bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {

        PlayCutscene(GameManager.Instance.CurrentCutscene);
    }

    private void Update()
    {
        if (waiting && Input.GetKeyUp(KeyCode.Z)) 
        {
            waiting = false;
            Director.Resume();
            WaitPrompt.SetActive(false);
        }
    }

    void PlayCutscene(string cutsceneName)
    {
        _imageIndex = 0;
        _textIndex = 0;
        _animationIndex = 0;

        Cutscene c = CutsceneList.Find(a => a.ID == cutsceneName);
        if (c != null)
        {
            if (c.ShowBox1) ShowBox(DialogueBoxList[0]);
            if (c.ShowBox2) ShowBox(DialogueBoxList[1]);
            if (c.ShowBox3) ShowBox(DialogueBoxList[2]);

            _currentCutscene = Instantiate(c);
            Director.playableAsset = _currentCutscene.Timing;
            Director.Play();
        }
    }

    public void CloseAllBoxes()
    {
        foreach (DialogueTextBox box in DialogueBoxList)
        {
            HideBox(box);
        }
    }

    public void Advance()
    {
        if (_imageIndex < _currentCutscene.Slideshow.Count)
        {
            SlideshowHost.sprite = _currentCutscene.Slideshow[_imageIndex];
            _imageIndex++;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1, 0.3f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0, 0.3f));
    }

    public void LoadScene()
    {
        GameManager.Instance.LoadScene(_currentCutscene.SceneToLoad);
    }

    private IEnumerator Fade(float from, float to, float time)
    {
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            GameManager.Instance.GBMaterial.SetFloat("_Fade", Mathf.Lerp(from, to, currentTime / time));
            yield return new WaitForEndOfFrame();
        }
    }

    public void AdvanceText()
    {
        if (_textIndex < _currentCutscene.Text.Count)
        {
            Dialogue d = _currentCutscene.Text[_textIndex];

            DialogueBoxList.ForEach(a => a.ClearText());
            DialogueBoxList[d.Box].SetText(d.Text);
        }

        _textIndex++;
    }

    public void AdvanceAnimation()
    {
        if (_animationIndex < _currentCutscene.Animations.Count)
        {
            CutsceneAnimation d = _currentCutscene.Animations[_animationIndex];

            StartCoroutine(SpawnAnimation(d));
        }

        _animationIndex++;
    }

    IEnumerator SpawnAnimation(CutsceneAnimation animation) {
        GameObject g = Instantiate(animation.Prefab, animation.Location, Quaternion.identity);

        yield return new WaitForSeconds(animation.Time);

        Destroy(g);
    }

    public void WaitForInput()
    {
        if (!waiting)
        {
            waiting= true;
            Director.Pause();
            WaitPrompt.SetActive(true);
        }
    }

    private void ShowBox(DialogueTextBox box)
    {
        box.gameObject.SetActive(true);
    }

    private void HideBox(DialogueTextBox box)
    {
        box.gameObject.SetActive(false);
    }
}
