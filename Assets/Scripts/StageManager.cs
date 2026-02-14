using UnityEngine;

public static class StageManager
{
    public static int CurrentStage = 1;
    public const int TotalStages = 3;

    // ステージごとの固定敵配置（30体）
    static readonly Vector3[] Stage1Positions = new Vector3[]
    {
        new Vector3( 5.0f,  0.0f, 0), new Vector3( 7.0f,  1.5f, 0), new Vector3( 9.0f, -1.5f, 0),
        new Vector3(11.0f,  2.0f, 0), new Vector3(13.0f,  0.0f, 0), new Vector3(15.0f, -2.0f, 0),
        new Vector3(17.0f,  1.0f, 0), new Vector3(19.0f, -1.0f, 0), new Vector3(21.0f,  2.0f, 0),
        new Vector3(23.0f,  0.0f, 0), new Vector3(25.0f, -1.5f, 0), new Vector3(27.0f,  1.5f, 0),
        new Vector3(29.0f, -2.0f, 0), new Vector3(31.0f,  1.0f, 0), new Vector3(33.0f,  0.0f, 0),
        new Vector3(35.0f, -1.0f, 0), new Vector3(37.0f,  2.0f, 0), new Vector3(39.0f, -1.5f, 0),
        new Vector3(41.0f,  1.5f, 0), new Vector3(43.0f,  0.0f, 0), new Vector3( 6.0f, -1.0f, 0),
        new Vector3(10.0f,  2.0f, 0), new Vector3(14.0f, -1.5f, 0), new Vector3(18.0f,  1.5f, 0),
        new Vector3(22.0f, -2.0f, 0), new Vector3(26.0f,  0.5f, 0), new Vector3(30.0f, -0.5f, 0),
        new Vector3(34.0f,  1.5f, 0), new Vector3(38.0f, -1.0f, 0), new Vector3(42.0f,  2.0f, 0),
    };

    // ステージ2：Y方向の幅が広い（-4〜4）
    static readonly Vector3[] Stage2Positions = new Vector3[]
    {
        new Vector3( 5.0f,  3.0f, 0), new Vector3( 7.0f, -3.0f, 0), new Vector3( 9.0f,  4.0f, 0),
        new Vector3(11.0f, -4.0f, 0), new Vector3(13.0f,  2.0f, 0), new Vector3(15.0f, -2.0f, 0),
        new Vector3(17.0f,  3.5f, 0), new Vector3(19.0f, -3.5f, 0), new Vector3(21.0f,  4.0f, 0),
        new Vector3(23.0f, -1.0f, 0), new Vector3(25.0f,  2.0f, 0), new Vector3(27.0f, -4.0f, 0),
        new Vector3(29.0f,  3.0f, 0), new Vector3(31.0f, -2.5f, 0), new Vector3(33.0f,  4.0f, 0),
        new Vector3(35.0f, -3.5f, 0), new Vector3(37.0f,  1.5f, 0), new Vector3(39.0f, -4.0f, 0),
        new Vector3(41.0f,  3.0f, 0), new Vector3(43.0f, -2.0f, 0), new Vector3( 6.0f, -1.5f, 0),
        new Vector3(10.0f,  3.5f, 0), new Vector3(14.0f, -3.0f, 0), new Vector3(18.0f,  2.5f, 0),
        new Vector3(22.0f, -4.0f, 0), new Vector3(26.0f,  3.5f, 0), new Vector3(30.0f, -1.5f, 0),
        new Vector3(34.0f,  4.0f, 0), new Vector3(38.0f, -2.5f, 0), new Vector3(42.0f,  1.0f, 0),
    };

    // ステージ3：密集グループ配置
    static readonly Vector3[] Stage3Positions = new Vector3[]
    {
        new Vector3( 5.0f,  2.0f, 0), new Vector3( 5.5f, -2.0f, 0), new Vector3( 6.0f,  4.0f, 0),
        new Vector3( 9.0f, -3.5f, 0), new Vector3( 9.5f,  1.5f, 0), new Vector3(10.0f, -1.0f, 0),
        new Vector3(13.0f,  3.0f, 0), new Vector3(13.5f, -4.0f, 0), new Vector3(14.0f,  1.0f, 0),
        new Vector3(17.0f, -2.5f, 0), new Vector3(17.5f,  3.5f, 0), new Vector3(18.0f, -1.0f, 0),
        new Vector3(21.0f,  4.0f, 0), new Vector3(21.5f, -3.0f, 0), new Vector3(22.0f,  2.0f, 0),
        new Vector3(25.0f, -4.0f, 0), new Vector3(25.5f,  2.5f, 0), new Vector3(26.0f, -1.5f, 0),
        new Vector3(29.0f,  3.5f, 0), new Vector3(29.5f, -2.0f, 0), new Vector3(30.0f,  4.0f, 0),
        new Vector3(33.0f, -3.5f, 0), new Vector3(33.5f,  1.5f, 0), new Vector3(34.0f, -2.0f, 0),
        new Vector3(37.0f,  3.0f, 0), new Vector3(37.5f, -4.0f, 0), new Vector3(38.0f,  2.0f, 0),
        new Vector3(41.0f, -2.5f, 0), new Vector3(41.5f,  4.0f, 0), new Vector3(42.0f, -1.5f, 0),
    };

    public static Vector3[] GetEnemyPositions()
    {
        switch (CurrentStage)
        {
            case 1: return Stage1Positions;
            case 2: return Stage2Positions;
            case 3: return Stage3Positions;
            default: return Stage1Positions;
        }
    }
}
