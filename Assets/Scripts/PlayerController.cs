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
        // ©“®‘Oi
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        // ƒXƒy[ƒXƒL[‚ÅUŒ‚
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        // –Ú‚Ì‘O‚Ì“G‚ğ’T‚·
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange)
            {
                // Timeline‰‰oÄ¶(“G‚ÌˆÊ’u‚ğ“n‚·)
                hitEffect.PlayHitEffect(enemy.transform.position);

                // “G‚ğ“|‚µ‚ÄƒXƒRƒA‰ÁZ
                enemy.Die();
                gameManager.AddScore(100);
                break; // 1‘Ì‚¾‚¯“|‚·
            }
        }
    }
}