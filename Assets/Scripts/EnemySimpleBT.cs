using UnityEngine;

// Behavior Tree パターンを使用した敵AI
public class EnemySimpleBT : MonoBehaviour
{
    private enum State { Idle, Chasing, Attacking, Destroyed }
    private State currentState = State.Idle;

    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float attackDistance = 0.5f;

    private float speed;
    private Transform player;
    private bool hasAttacked = false;

    void Start()
    {
        // ランダムな速度を設定
        speed = Random.Range(minSpeed, maxSpeed);

        // プレイヤーを探す
        PlayerController playerCtrl = FindFirstObjectByType<PlayerController>();
        if (playerCtrl != null)
            player = playerCtrl.transform;

        currentState = State.Chasing;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                // 攻撃後は動かない
                break;
        }
    }

    void Chase()
    {
        if (player == null) return;

        // プレイヤーの現在位置に向かって移動
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        // 十分近づいたら攻撃
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackDistance)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (hasAttacked) return;
        hasAttacked = true;
        currentState = State.Attacking;

        PlayerController playerCtrl = player.GetComponent<PlayerController>();
        if (playerCtrl != null)
            playerCtrl.TakeDamage();
    }

    public void OnDestroyed()
    {
        currentState = State.Destroyed;
    }
}
