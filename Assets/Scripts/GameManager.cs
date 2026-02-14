using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private int enemiesKilled = 0;
    private int currentHp = 3;
    private int maxHp = 3;
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

    public void UpdateHpUI(int hp, int max)
    {
        currentHp = hp;
        maxHp = max;
        UpdateScoreUI();
    }

    public void ReachGoal()
    {
        resultManager.ShowResult(score, "GOAL!");
    }

    public void GameOver()
    {
        resultManager.ShowResult(score, "GAME OVER");
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score + "\nKilled: " + enemiesKilled + "\nHP: " + currentHp + " / " + maxHp;
    }
}
