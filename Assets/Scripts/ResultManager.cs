using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void ShowResult(int score)
    {
        resultPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        Time.timeScale = 0f; // ÉQÅ[ÉÄí‚é~
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ÉQÅ[ÉÄçƒäJ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}