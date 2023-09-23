using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string currentLoadedScene = "";
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    [SerializeField] public MeshRenderer GBMesh;
    [HideInInspector] public Material GBMaterial;

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
            Destroy(this);
        } else
        {
            _instance = this;
        }

        GBMaterial = GBMesh.material;
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
        yield return Fade(1, 0, 1);

        if (currentLoadedScene != "")
        {
            yield return SceneManager.UnloadSceneAsync(currentLoadedScene);
        }
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        currentLoadedScene = scene;

        yield return Fade(0, 1, 1);
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
}