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

    private bool _levelSelect;

    [SerializeField] public MeshRenderer GBMesh;
    [HideInInspector] public Material GBMaterial;

    public string CurrentCutscene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
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

    public void LoadLevel(string sceneName)
    {
        string n = _levelSelect ? "LevelSelect" : sceneName;
        LoadScene(n);
    }

    public void ResetLevel()
    {
        LoadScene(currentLoadedScene);
    }

    private IEnumerator LoadAnimation(string scene)
    {
        string sceneToDelete = currentLoadedScene;
        currentLoadedScene = scene;

        yield return Fade(1, 0, 1);

        if (sceneToDelete != "")
        {
            yield return SceneManager.UnloadSceneAsync(sceneToDelete);
        }
        
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

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