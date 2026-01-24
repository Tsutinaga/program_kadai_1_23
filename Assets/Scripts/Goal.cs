using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            gm.ReachGoal();
        }
    }
}