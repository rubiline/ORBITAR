using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PauseControl : MonoBehaviour
{
    public InputActionReference PauseButton;
    public InputManager Gameplay;
    public LevelManager LevelManager;
    public GameObject PauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        PauseButton.action.performed += PauseGame;
    }

    private void OnDestroy()
    {
        PauseButton.action.performed -= PauseGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(CallbackContext ctx)
    {
        if (Gameplay != null)
        {
            if (!LevelManager.Paused)
            {
                ToMenu();
            } 
        }
    }

    public void Register()
    {
        PauseButton.action.Enable();
        Gameplay = FindFirstObjectByType<InputManager>();
        LevelManager = FindFirstObjectByType<LevelManager>();
        PauseMenu = FindFirstObjectByType<PauseMenu>(FindObjectsInactive.Include).transform.parent.gameObject;
    }

    public void Deregister()
    {
        PauseButton.action.Disable();
        Gameplay?.EnableGameplayControls(false);
        Gameplay = null;
        LevelManager = null;
        PauseMenu = null;
    }

    public void ToMenu()
    {
        LevelManager.Paused = true;
        Gameplay?.EnableGameplayControls(false);
        PauseMenu.SetActive(true);
    }

    public void ToGameplay()
    {
        LevelManager.Paused = false;
        Gameplay?.EnableGameplayControls(true);
        PauseMenu.SetActive(false);
    }
}
