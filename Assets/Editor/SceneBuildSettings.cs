using UnityEditor;
using UnityEngine;

/// <summary>
/// プロジェクトを開いた際に必要なシーンをBuild Settingsへ自動登録する
/// </summary>
[InitializeOnLoad]
public class SceneBuildSettings
{
    static readonly string[] ScenePaths = new string[]
    {
        "Assets/Scenes/Title.unity",
        "Assets/Scenes/Stage1.unity",
        "Assets/Scenes/Stage2.unity",
        "Assets/Scenes/Stage3.unity",
    };

    static SceneBuildSettings()
    {
        var existing = EditorBuildSettings.scenes;
        bool changed = false;

        var list = new System.Collections.Generic.List<EditorBuildSettingsScene>(existing);

        foreach (var path in ScenePaths)
        {
            bool found = false;
            foreach (var s in list)
            {
                if (s.path == path) { found = true; break; }
            }
            if (!found)
            {
                list.Add(new EditorBuildSettingsScene(path, true));
                changed = true;
                Debug.Log("[SceneBuildSettings] Added to Build Settings: " + path);
            }
        }

        if (changed)
            EditorBuildSettings.scenes = list.ToArray();
    }
}
