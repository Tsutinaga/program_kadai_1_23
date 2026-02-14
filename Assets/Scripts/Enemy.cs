using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // EnemySimpleBT に死亡を通知
        GetComponent<EnemySimpleBT>()?.OnDestroyed();

        // 白フラッシュしてから削除
        spriteRenderer.color = Color.white;
        Destroy(gameObject, 0.1f);
    }
}
