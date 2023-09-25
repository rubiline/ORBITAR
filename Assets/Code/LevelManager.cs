using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public string music;
    public TimeSpan goldTime;
    public TimeSpan silverTime;
    public TimeSpan bronzeTime;

    public string goldTimeString;
    public string silverTimeString;
    public string bronzeTimeString;

    public int totalGems;
    public int collectedGems;
    public TimeSpan currentTime;
    public Stopwatch timer;
    public TextMeshProUGUI timerText;
    public LevelResult levelResult;

    private LevelStartScreen lss;

    public Tutorial tutorial;

    public static LevelManager Instance => _instance;
    private static LevelManager _instance;

    public bool Paused
    {
        get
        {
            return _paused;
        }
        set
        {
            _paused = value;
            OnPause(_paused);
        }
    }
    private bool _paused;

    public PlayerController PlayerController;

    private void Awake()
    {
        _instance = this;
        tutorial = FindFirstObjectByType<Tutorial>(FindObjectsInactive.Include);

        TimeSpan.TryParseExact(goldTimeString, @"m\:ss\.fff", CultureInfo.CurrentCulture, out goldTime);
        TimeSpan.TryParseExact(silverTimeString, @"m\:ss\.fff", CultureInfo.CurrentCulture, out silverTime);
        TimeSpan.TryParseExact(bronzeTimeString, @"m\:ss\.fff", CultureInfo.CurrentCulture, out bronzeTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = FindFirstObjectByType<PlayerController>();

        string stage = GameManager.Instance.currentLoadedScene;
        if (!GameManager.Instance._levelSelect) PlayerPrefs.SetString("continue", stage);

        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        lss = FindFirstObjectByType<LevelStartScreen>(FindObjectsInactive.Include);
        lss.gameObject.SetActive(true);
        lss.SetLevelTitle(levelName);

        if (music != "" && AudioManager.Instance.targetSong.name != music)
        {
            AudioManager.Instance.PlayMusic(music);
        }

        if (tutorial.gameObject.activeInHierarchy)
        {
            tutorial.Advance();
            while (!tutorial.IsDone)
            {
                yield return null;
            }

            tutorial.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        lss.GoTime();
        GameManager.Instance.PauseControl.Register();
        GameManager.Instance.PauseControl.ToGameplay();


        yield return null;
    }

    public void FirstInput()
    {
        lss.gameObject.SetActive(false);
        StartTimer();
        currentTime = new TimeSpan(0);
    }

    public void QuitLevel()
    {
        if (timer != null) timer.Stop();
        // TODO: STOP MUSIC
        GameManager.Instance.QuitLevel();
    }

    public void OnPause(bool pause)
    {
        PlayerController.freeze = pause;
        if (lss != null && pause)
        {
            lss.gameObject.SetActive(false);
        }
        
        if (timer != null)
        {
            if (pause)
            {
                timer.Stop();
            } else
            {
                timer.Start();
            }
        }
    }

    public LevelResult Victory()
    {
        LevelResult res = LevelResult.NONE;

        if (currentTime < goldTime)
        {
            res = LevelResult.GOLD;
        } else if (currentTime < silverTime)
        {
            res = LevelResult.SILVER;
        } else if (currentTime < bronzeTime)
        {
            res = LevelResult.BRONZE;
        }

        string stage = GameManager.Instance.currentLoadedScene;

        PlayerPrefs.SetInt(stage + "_completed", 1);

        string highScore = PlayerPrefs.GetString(stage + "_time", "");
        TimeSpan highScoreTime;
        
        if (highScore != "" && TimeSpan.TryParseExact(highScore, @"m\:ss\.fff", CultureInfo.CurrentCulture, out highScoreTime) && highScoreTime > currentTime)
        {
            PlayerPrefs.SetString(stage + "_time", currentTime.ToString(@"m\:ss\.fff"));
            PlayerPrefs.SetInt(stage + "_medal", (int)res);
        } else if (highScore == "")
        {
            PlayerPrefs.SetString(stage + "_time", currentTime.ToString(@"m\:ss\.fff"));
            PlayerPrefs.SetInt(stage + "_medal", (int)res);
        }

        return res;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer != null)
        {
            currentTime = timer.Elapsed;
        }
        timerText.text = currentTime.ToString(@"m\:ss\.fff");
    }

    private void OnDestroy()
    {
        if (timer != null) timer.Stop();
    }

    private void StartTimer()
    {
        timer = new Stopwatch();
        timer.Start();
    }

    public enum LevelResult
    {
        NONE,
        GOLD,
        SILVER,
        BRONZE
    }
}
