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

        // Use this for initialization
        void Start()
        {
            player = FindFirstObjectByType<PlayerController>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Finish()
        {
            boxCollider.enabled = false;
            player.freeze = true;

            // TODO: Victory Screen!

            if (ToLevel.Equals("Cutscene"))
            {
                GameManager.Instance.CurrentCutscene = CutsceneName;
            }
            GameManager.Instance.LoadLevel(ToLevel);
        }
    }
}