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

        // ”’‚­ƒtƒ‰ƒbƒVƒ…‚³‚¹‚é
        spriteRenderer.color = Color.white;

        // 0.1•bŒã‚ÉÁ‚·
        Destroy(gameObject, 0.1f);
    }
}