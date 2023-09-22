using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string currentLoadedScene;
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    [SerializeField] public Material GBMaterial;

    public string CurrentCutscene;

    public int[] Level
    {
        get { return _level; }
        set { _level = value; LoadLevel(value); }
    }

    private int[] _level = new int[2];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        _instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAnimation(sceneName));
    }

    public void LoadLevel(int[] level)
    {
        LoadScene("Stage " + level[0] + "-" + level[1]);
    }

    private IEnumerator LoadAnimation(string scene)
    {
        while (GBMaterial.GetFloat("Fade") > 0)
        {
            GBMaterial.SetFloat("Fade", GBMaterial.GetFloat("Fade") - Time.deltaTime);
            yield return null;
        }

        if (currentLoadedScene != "")
        {
            yield return SceneManager.UnloadSceneAsync(currentLoadedScene);
        }
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        currentLoadedScene = scene;

        while (GBMaterial.GetFloat("Fade") < 1)
        {
            GBMaterial.SetFloat("Fade", GBMaterial.GetFloat("Fade") + Time.deltaTime);
            yield return null;
        }
    }
}