using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void ShowResult(int score, string title)
    {
        resultPanel.SetActive(true);
        int stage = StageManager.CurrentStage;
        finalScoreText.text = title + "\nFinal Score: " + score;
        Time.timeScale = 0f;

        // 「次のステージ」ボタンをコードで追加
        if (stage < StageManager.TotalStages)
        {
            AddButton("NEXT STAGE", new Color(0.15f, 0.6f, 0.3f), new Vector2(0, -80), NextStage);
        }

        // 「タイトルへ」ボタンを追加
        AddButton("TITLE", new Color(0.4f, 0.4f, 0.5f), new Vector2(0, -170), GoToTitle);
    }

    void AddButton(string label, Color color, Vector2 offset, UnityEngine.Events.UnityAction action)
    {
        // resultPanel内の基準位置（既存のRestartボタンの下に追加）
        GameObject btnObj = new GameObject(label + "Button");
        btnObj.transform.SetParent(resultPanel.transform, false);

        RectTransform rt = btnObj.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = offset;
        rt.sizeDelta = new Vector2(320, 70);

        Image img = btnObj.AddComponent<Image>();
        img.color = color;

        Button btn = btnObj.AddComponent<Button>();
        btn.onClick.AddListener(action);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(btnObj.transform, false);
        RectTransform trt = textObj.AddComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.sizeDelta = Vector2.zero;

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 36;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        tmp.fontStyle = FontStyles.Bold;
    }

    // シーン内の既存「Restart」ボタンに割り当て済み
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextStage()
    {
        Time.timeScale = 1f;
        StageManager.CurrentStage++;
        SceneManager.LoadScene("SampleScene");
    }

    void GoToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
