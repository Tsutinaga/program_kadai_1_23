using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int maxHp = 3;

    private int hp;
    private GameManager gameManager;
    private HitEffect hitEffect;

    void Start()
    {
        hp = maxHp;
        gameManager = FindFirstObjectByType<GameManager>();
        hitEffect = FindFirstObjectByType<HitEffect>();
        gameManager.UpdateHpUI(hp, maxHp);
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
        // 近くの敵を探す
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange)
            {
                // Timeline再生(敵の位置を渡す)
                hitEffect.PlayHitEffect(enemy.transform.position);

                // 敵を倒してスコア加算
                enemy.Die();
                gameManager.AddScore(100);
                break; // 1体だけ倒す
            }
        }
    }

    public void TakeDamage()
    {
        hp--;
        gameManager.UpdateHpUI(hp, maxHp);

        if (hp <= 0)
            gameManager.GameOver();
    }
}
