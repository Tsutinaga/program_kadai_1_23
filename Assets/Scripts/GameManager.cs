using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private int enemiesKilled = 0;
    private ResultManager resultManager;

    void Start()
    {
        resultManager = FindFirstObjectByType<ResultManager>();
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        enemiesKilled++;
        UpdateScoreUI();
    }

    public void ReachGoal()
    {
        resultManager.ShowResult(score, "GOAL!");
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score + "\nKilled: " + enemiesKilled;
    }
}
