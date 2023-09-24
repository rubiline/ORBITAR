using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentLoadedScene = "";
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    public bool _levelSelect;

    [SerializeField] public MeshRenderer GBMesh;
    [HideInInspector] public Material GBMaterial;

    public string CurrentCutscene;
    public PauseControl PauseControl;

    public List<Sprite> palettes;
    [SerializeField] private int paletteIdx = 0;

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
        GBMaterial.SetTexture("_Palette", palettes[paletteIdx].texture);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAnimation(sceneName));
    }

    public void LoadLevel(string sceneName, bool select = false)
    {
        PauseControl.Deregister();
        string n = (_levelSelect) ? "LevelSelect" : sceneName;
        if (select) _levelSelect = true;
        LoadScene(n);
    }

    public void QuitLevel()
    {
        PauseControl.Deregister();
        string n = _levelSelect ? "LevelSelect" : "Title";
        LoadScene(n);
    }

    public void ResetLevel()
    {
        LoadScene(currentLoadedScene);
    }

    public void SwapPalette()
    {
        paletteIdx = (paletteIdx + 1) % palettes.Count;
        GBMaterial.SetTexture("_Palette", palettes[paletteIdx].texture);
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