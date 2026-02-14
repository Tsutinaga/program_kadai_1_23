using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    // 敵同士の最小到着間隔（秒）- タイミングゲームとして快適な値
    [SerializeField] private float minArrivalGap = 0.7f;

    // 推定計算に使う定数
    private const float PlayerSpeed   = 2f;   // PlayerController.moveSpeed と合わせる
    private const float AvgEnemySpeed = 2.5f; // EnemySimpleBT の速度の中央値

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        Vector3[] positions = StageManager.GetEnemyPositions();

        // 推定到着時間を計算してソート
        var sorted = positions
            .Select(pos => new { pos, eta = EstimateArrivalTime(pos) })
            .OrderBy(e => e.eta)
            .ToArray();

        float lastArrival = -minArrivalGap;

        foreach (var entry in sorted)
        {
            // 前の敵との到着間隔が minArrivalGap 未満なら startDelay で遅延
            float desiredArrival = Mathf.Max(entry.eta, lastArrival + minArrivalGap);
            float spawnDelay     = Mathf.Max(0f, desiredArrival - entry.eta);

            var go = Instantiate(enemyPrefab, entry.pos, Quaternion.identity);
            go.GetComponent<EnemySimpleBT>()?.SetStartDelay(spawnDelay);

            lastArrival = desiredArrival;
        }
    }

    // 各スポーン位置からプレイヤーへの推定到着時間
    float EstimateArrivalTime(Vector3 pos)
    {
        if (pos.x >= 0f)
        {
            // 前方スポーン: プレイヤーが右に走って追いつく
            return pos.x / PlayerSpeed;
        }
        else
        {
            // 背後スポーン: 敵がプレイヤーに追いつく（相対速度で近似）
            float relSpeed = AvgEnemySpeed - PlayerSpeed;
            if (relSpeed <= 0f) return float.MaxValue;
            return Mathf.Abs(pos.x) / relSpeed;
        }
    }
}
