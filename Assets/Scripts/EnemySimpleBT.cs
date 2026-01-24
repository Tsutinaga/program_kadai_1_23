using UnityEngine;

// Behavior Tree パターンを使用した敵AI
public class EnemySimpleBT : MonoBehaviour
{
    private enum State { Idle, Destroyed }
    private State currentState = State.Idle;

    void Update()
    {
        // Behavior Tree: シンプルな状態遷移
        switch (currentState)
        {
            case State.Idle:
                // アイドル状態: 何もしない
                break;
            case State.Destroyed:
                // 破壊された状態
                break;
        }
    }

    public void OnDestroyed()
    {
        currentState = State.Destroyed;
    }
}