using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public List<World> stages;

    private Dictionary<string, bool> LevelCompleted = new Dictionary<string, bool>();
    private Dictionary<string, int> LevelMedal = new Dictionary<string, int>();
    private Dictionary<string, string> LevelTime = new Dictionary<string, string>();

    private List<MenuItem> worldMenu = new List<MenuItem>();
    private Dictionary<int, List<MenuItem>> worldMenuItems = new Dictionary<int, List<MenuItem>>();

    public GameObject menuPrefab;
    public GameObject boxPrefab;

    private TextBox description;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("SELECT_NOVA");
        Action<string> Play = (a) =>
        {
            GameManager.Instance.LoadLevel(a, true);
        };

        Action<int> OpenWorld = (a) =>
        {
            List<MenuItem> stagesInWorld = worldMenuItems[a];

            MenuManager.Instance.CreateMenuChild(menuPrefab, "", stagesInWorld, 4, (new Vector2(3, 0)));
        };

        Action<string> CheckStage = (a) =>
        {
            int medal = LevelMedal[a];
            string time = LevelTime[a];

            string rank = "";
            
            switch (medal)
            {
                case 1:
                    rank = "GOLD";
                    break;                
                case 2:
                    rank = "SILVER";
                    break;                
                case 3:
                    rank = "BRONZE";
                    break;
                default:
                    rank = "UNRANKED";
                    break;
            }

            string desc = "" + a + "\nTIME:" + time + "\nRANK:" + rank;

            description = MenuManager.Instance.CreateTextBox(boxPrefab, desc, 4, 2, new Vector2(6, 0));
        };

        Action DestroyDesc = () =>
        {
            Destroy(description.gameObject);
        };

        worldMenu.Add(new MenuItem("RETURN", () => GameManager.Instance.LoadScene("Title")));

        int worldIdx = 0;
        foreach (World world in stages)
        {
            int stagesCompleted = 0;
            worldMenuItems[worldIdx] = new List<MenuItem>();
            foreach (string stage in world.stages)
            {
                bool completed = PlayerPrefs.GetInt(stage + "_completed", 0) == 1;
                if (completed)
                {
                    stagesCompleted++;
                    worldMenuItems[worldIdx].Add(new MenuItem(stage, () => Play.Invoke(stage), () => CheckStage.Invoke(stage), DestroyDesc));
                    LevelCompleted.Add(stage, true);
                    LevelMedal.Add(stage, PlayerPrefs.GetInt(stage + "_medal"));
                    LevelTime.Add(stage, PlayerPrefs.GetString(stage + "_time"));
                }
            }

            if (stagesCompleted > 0)
            {
                int idx = worldIdx;
                worldMenu.Add(new MenuItem("World " + (worldIdx + 1), () => OpenWorld.Invoke(idx)));
            } else
            {
                worldMenu.Add(new MenuItem("-----", null));
            }

            worldIdx++;
        }

        MenuManager.Instance.CreateMenu(menuPrefab, "", worldMenu, 3, Vector2.zero);
    }

    [Serializable]
    public class World
    {
        public List<string> stages;
    }
}
