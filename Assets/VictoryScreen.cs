using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI GoldTime;
    public TextMeshProUGUI SilverTime;
    public TextMeshProUGUI BronzeTime;
    public TextMeshProUGUI YourTime;

    public GameObject GoldMedal;
    public GameObject SilverMedal;
    public GameObject BronzeMedal;

    private string toLevel;
    private string cutscene;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) OnSelect();
    }

    // Update is called once per frame
    void OnSelect()
    {
        if (toLevel.Equals("Cutscene"))
        {
            GameManager.Instance.CurrentCutscene = cutscene;
        }
        GameManager.Instance.LoadLevel(toLevel);
    }

    public void Setup(string toLevel, string cutscene)
    {
        this.toLevel = toLevel;
        this.cutscene = cutscene;

        GameManager.Instance.PauseControl.Deregister();

        GoldTime.text = LevelManager.Instance.goldTime.ToString(@"m\:ss\.fff");
        SilverTime.text = LevelManager.Instance.silverTime.ToString(@"m\:ss\.fff");
        BronzeTime.text = LevelManager.Instance.bronzeTime.ToString(@"m\:ss\.fff");
        YourTime.text = LevelManager.Instance.currentTime.ToString(@"m\:ss\.fff");

        LevelManager.LevelResult res = LevelManager.Instance.Victory();

        GameObject g = null;
        switch (res)
        {
            case LevelManager.LevelResult.GOLD:
                {
                    g = GoldMedal;
                    break;
                }
            case LevelManager.LevelResult.SILVER:
                {
                    g = SilverMedal;
                    break;
                }
            case LevelManager.LevelResult.BRONZE:
                {
                    g = BronzeMedal;
                    break;
                }
            default:
                {
                    break;
                }
        }

        if (g != null)
        {
            g.SetActive(true);
        }
    }
}
