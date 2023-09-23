using System.Collections;
using UnityEngine;

namespace Assets
{
    public class FinishLine : MonoBehaviour
    {
        public string ToLevel;
        public string CutsceneName;

        private PlayerController player;
        private BoxCollider2D boxCollider;
        private VictoryScreen victoryScreen;

        // Use this for initialization
        void Start()
        {
            player = FindFirstObjectByType<PlayerController>();
            boxCollider = GetComponent<BoxCollider2D>();
            victoryScreen = FindFirstObjectByType<VictoryScreen>(FindObjectsInactive.Include);
        }

        public void Finish()
        {
            boxCollider.enabled = false;

            LevelManager.Instance.OnPause(true);
            victoryScreen.gameObject.SetActive(true);
            victoryScreen.Setup(ToLevel, CutsceneName);
        }
    }
}