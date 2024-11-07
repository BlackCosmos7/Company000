using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleEffect : MonoBehaviour
{
    public SpriteRenderer puddleRenderer;
    private float fadeDuration = 1f;

    void Start()
    {
        // Начинаем с полной прозрачности
        puddleRenderer.color = new Color(1, 1, 1, 0);
    }

    public void ActivatePuddle(Vector3 position)
    {
        transform.position = position;
        StartCoroutine(FadeInPuddle());
    }

    private IEnumerator FadeInPuddle()
    {
        float timeElapsed = 0;
        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            puddleRenderer.color = new Color(1f, 1f, 1f, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        puddleRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}