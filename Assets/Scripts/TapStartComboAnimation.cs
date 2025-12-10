using UnityEngine;
public class TapStartComboAnimation : MonoBehaviour
{
    RectTransform rect;
    Vector3 startScale;
    Vector2 startPos;

    public float pulseAmount = 0.1f;
    public float pulseSpeed = 3f;
    public float bounceAmount = 30f;
    public float bounceSpeed = 3f;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startScale = transform.localScale;
        startPos = rect.anchoredPosition;
    }
    void Update()
    {
        // ÖNEMLÝ: timeScale'den etkilenmemesi için unscaledTime kullanýyoruz
        float t = Time.unscaledTime;

        // SCALE PULSE
        float s = Mathf.Sin(t * pulseSpeed) * pulseAmount;
        transform.localScale = startScale * (1 + s);

        // BOUNCE
        float y = Mathf.Sin(t * bounceSpeed) * bounceAmount;
        rect.anchoredPosition = startPos + new Vector2(0, y);
    }
}

