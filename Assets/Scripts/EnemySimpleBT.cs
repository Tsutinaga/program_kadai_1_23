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
    private Vector3 moveDirection; // 開始時に1回だけ計算した方向

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        PlayerController playerCtrl = FindFirstObjectByType<PlayerController>();
        if (playerCtrl != null)
        {
            player = playerCtrl.transform;
            CalculateInterceptDirection();
        }
    }

    // 二次方程式で正確なインターセプト方向を1回だけ計算
    void CalculateInterceptDirection()
    {
        Vector3 enemyPos = transform.position;
        Vector3 playerPos = player.position;

        float dx = playerPos.x - enemyPos.x;
        float dy = playerPos.y - enemyPos.y;
        float vs = playerSpeed; // プレイヤー速度（右方向のみ）

        // 解く方程式:  |enemyPos + d * t - (playerPos + (vs*t, 0))|² = 0
        // → (dx + vs*t)² + dy² = (speed*t)²
        // → (vs² - speed²)t² + 2*dx*vs*t + (dx² + dy²) = 0
        float a = vs * vs - speed * speed;
        float b = 2f * dx * vs;
        float c = dx * dx + dy * dy;

        float interceptTime = -1f;

        if (Mathf.Abs(a) < 0.001f)
        {
            // a≒0（速度が同じ）: 線形方程式
            if (Mathf.Abs(b) > 0.001f)
                interceptTime = -c / b;
        }
        else
        {
            float disc = b * b - 4f * a * c;
            if (disc >= 0f)
            {
                float t1 = (-b + Mathf.Sqrt(disc)) / (2f * a);
                float t2 = (-b - Mathf.Sqrt(disc)) / (2f * a);

                // 正の解のうち小さい方を採用
                if (t1 > 0f && t2 > 0f)
                    interceptTime = Mathf.Min(t1, t2);
                else if (t1 > 0f)
                    interceptTime = t1;
                else if (t2 > 0f)
                    interceptTime = t2;
            }
        }

        Vector3 targetPos;
        if (interceptTime > 0f)
        {
            // 正確なインターセプト位置
            targetPos = playerPos + Vector3.right * vs * interceptTime;
        }
        else
        {
            // 解なし（追いつけない）→ 現在位置を直接狙う
            targetPos = playerPos;
        }

        moveDirection = (targetPos - enemyPos).normalized;
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
        if (distance <= passThroughDistance)
        {
            currentState = State.PassingThrough;
            return;
        }

        // 計算済みの方向に直進（毎フレーム再計算しない）
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void PassThrough()
    {
        // 同じ方向でそのまま直進してすり抜ける
        transform.position += moveDirection * speed * Time.deltaTime;

        if (player != null && Vector3.Distance(transform.position, player.position) > 30f)
            Destroy(gameObject);
    }

    public void OnDestroyed()
    {
        currentState = State.Destroyed;
    }
}
