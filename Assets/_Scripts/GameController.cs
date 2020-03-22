using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class GameController : MonoBehaviour
{
    public Text EndText;
    public GameObject RestartButton;
    public Salut salut;
    public Text LevelText;
    Save sv;
    bool gameEnded = false;
    private void Start()
    {
        float delta = 1f;
        delta = ((Screen.height * 1f) / (Screen.width * 1f)) / (16f / 9f);
        if (delta >= 0.98f)
        {
            transform.position = new Vector3(0f, 1f + 0.16f * (delta - 1f) / 0.21f, -3.28f - 0.75f * (delta - 1f) / 0.21f);
        }
        if (PlayerPrefs.HasKey("sv"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("sv"));
        }
        else
        {
            sv = new Save(0);
            SavePrefs();
        }
        Instantiate(Resources.Load<GameObject>("Level " + (sv.level < 10 ? sv.level : 9)));
        LevelText.text = "LEVEL " + (sv.level + 1);
        GameAnalytics.Initialize();
    }
    void SavePrefs()
    {
        PlayerPrefs.SetString("sv", JsonUtility.ToJson(sv));
    }
    public void Win()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            sv.level++;
            SavePrefs();
            salut.enabled = true;
            EndText.gameObject.SetActive(true);
            RestartButton.SetActive(true);
            GameAnalytics.NewDesignEvent("Level", sv.level);
        }
    }
    public void Lose()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            EndText.text = "LOSE!";
            RestartButton.transform.GetChild(0).GetComponent<Text>().text = "RESTART";
            EndText.gameObject.SetActive(true);
            RestartButton.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
class Save
{
    public int level;
    public Save(int level)
    {
        this.level = level;
    }
}
