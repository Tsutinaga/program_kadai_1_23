using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System.Collections;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private ParticleSystem hitParticle;

    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void PlayHitEffect(Vector3 position)
    {
        // Timelineを再生
        director.Play();

        // 画面フラッシュ
        StartCoroutine(FlashCoroutine());

        // パーティクル再生
        hitParticle.transform.position = position;
        hitParticle.Play();

        // ヒットストップ
        StartCoroutine(HitStopCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        // フラッシュイン(一瞬で真っ白に)
        Color color = flashImage.color;
        color.a = 0.8f;
        flashImage.color = color;

        // 0.05秒待つ
        yield return new WaitForSecondsRealtime(0.05f);

        // フラッシュアウト(徐々に透明に)
        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0.8f, 0f, elapsed / duration);
            flashImage.color = color;
            yield return null;
        }

        color.a = 0f;
        flashImage.color = color;
    }

    private IEnumerator HitStopCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
}