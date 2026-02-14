using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;

    private GameManager gameManager;
    private HitEffect hitEffect;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        hitEffect = FindFirstObjectByType<HitEffect>();
    }

    void Update()
    {
        // 右に前進
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        // スペースキーで攻撃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange)
            {
                hitEffect.PlayHitEffect(enemy.transform.position);
                enemy.Die();
                gameManager.AddScore(100);
                break;
            }
        }
    }
}
