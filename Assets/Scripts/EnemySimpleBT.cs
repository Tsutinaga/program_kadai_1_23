using UnityEngine;

// Behavior Tree パターンを使用した敵AI
public class EnemySimpleBT : MonoBehaviour
{
    private enum State { Chasing, PassingThrough, Destroyed }
    private State currentState = State.Chasing;

    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float passThroughDistance = 1f; // この距離以内に入ったらすり抜けに切り替え
    [SerializeField] private float playerSpeed = 2f;         // PlayerControllerのmoveSpeedと合わせる

    private float speed;
    private Transform player;
    private Vector3 lastDirection;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        PlayerController playerCtrl = FindFirstObjectByType<PlayerController>();
        if (playerCtrl != null)
            player = playerCtrl.transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Chasing:
                Chase();
                break;
            case State.PassingThrough:
                PassThrough();
                break;
        }
    }

    void Chase()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 一定距離以内に入ったらすり抜けモードに切り替え
        if (distance <= passThroughDistance)
        {
            currentState = State.PassingThrough;
            return;
        }

        // 先読み：自分が届くまでの時間にプレイヤーが進む位置を計算
        float timeToReach = distance / speed;
        Vector3 predictedPos = player.position + Vector3.right * playerSpeed * timeToReach;

        // 予測位置に向かって移動（方向を記録しておく）
        lastDirection = (predictedPos - transform.position).normalized;
        transform.position += lastDirection * speed * Time.deltaTime;
    }

    void PassThrough()
    {
        // 最後の方向にそのまま直進してすり抜ける
        transform.position += lastDirection * speed * Time.deltaTime;

        // 画面外に出たら削除
        if (Vector3.Distance(transform.position, player.position) > 30f)
            Destroy(gameObject);
    }

    public void OnDestroyed()
    {
        currentState = State.Destroyed;
    }
}
