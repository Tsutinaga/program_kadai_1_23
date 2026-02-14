using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        // EventSystem
        GameObject esObj = new GameObject("EventSystem");
        esObj.AddComponent<EventSystem>();
        esObj.AddComponent<StandaloneInputModule>();

        // Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        canvasObj.AddComponent<GraphicRaycaster>();

        // 背景
        CreatePanel(canvasObj.transform, new Color(0.05f, 0.05f, 0.15f));

        // タイトルロゴ
        CreateLabel(canvasObj.transform, "STAGE RUNNER", new Vector2(0, 280), 90, Color.white);

        // サブタイトル
        CreateLabel(canvasObj.transform, "- Select Stage -", new Vector2(0, 170), 36, new Color(0.8f, 0.8f, 0.8f));

        // ステージボタン
        CreateStageButton(canvasObj.transform, "STAGE  1", new Color(0.15f, 0.45f, 0.8f), new Vector2(0, 50), 1);
        CreateStageButton(canvasObj.transform, "STAGE  2", new Color(0.15f, 0.6f, 0.3f), new Vector2(0, -80), 2);
        CreateStageButton(canvasObj.transform, "STAGE  3", new Color(0.75f, 0.25f, 0.15f), new Vector2(0, -210), 3);
    }

    void CreatePanel(Transform parent, Color color)
    {
        GameObject obj = new GameObject("BG");
        obj.transform.SetParent(parent, false);
        Image img = obj.AddComponent<Image>();
        img.color = color;
        RectTransform rt = img.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;
    }

    void CreateLabel(Transform parent, string text, Vector2 pos, float size, Color color)
    {
        GameObject obj = new GameObject("Label");
        obj.transform.SetParent(parent, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(1000, 120);
        TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = size;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = color;
    }

    void CreateStageButton(Transform parent, string label, Color btnColor, Vector2 pos, int stage)
    {
        GameObject btnObj = new GameObject(label);
        btnObj.transform.SetParent(parent, false);
        RectTransform rt = btnObj.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(380, 90);
        Image img = btnObj.AddComponent<Image>();
        img.color = btnColor;
        Button btn = btnObj.AddComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.highlightedColor = btnColor * 1.3f;
        cb.pressedColor = btnColor * 0.7f;
        btn.colors = cb;
        int s = stage;
        btn.onClick.AddListener(() => StartStage(s));

        // ボタンラベル
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(btnObj.transform, false);
        RectTransform trt = textObj.AddComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.sizeDelta = Vector2.zero;
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 42;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        tmp.fontStyle = FontStyles.Bold;
    }

    void StartStage(int stage)
    {
        StageManager.CurrentStage = stage;
        SceneManager.LoadScene("SampleScene");
    }
}
