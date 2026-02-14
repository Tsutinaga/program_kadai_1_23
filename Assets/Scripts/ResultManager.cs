using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void ShowResult(int score, string title)
    {
        resultPanel.SetActive(true);
        finalScoreText.text = title + "\nFinal Score: " + score;
        Time.timeScale = 0f; // ゲーム停止
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ゲーム再開
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
