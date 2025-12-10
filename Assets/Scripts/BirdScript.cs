using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;  //  UI tıklamasını kontrol etmek için

public class BirdScript : MonoBehaviour
{
    public float ziplama_araligi;

    Rigidbody2D rb;

    public TMP_Text skor_text;
    public float skor;

    private bool isDead = false; //  öldü mü kontrolü

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skor = 0;
    }

    void Update()
    {
        // Eğer kuş öldüyse artık hiçbir şey yapma
        if (isDead)
            return;
        // Sadece oyun alanına tıklandığında zıplasın, UI'ya tıklanınca zıplamasın
        if (Input.GetMouseButtonDown(0))
        {
            //  Eğer tıklama bir UI elemanının üzerindeyse: hiçbir şey yapma
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            // Zıplama sesi
            AudioManager.instance.Play(AudioManager.instance.flapSound);

            // Zıplama
            rb.linearVelocity = Vector2.up * ziplama_araligi;
        }
        // Skor yazısını güncelle
        skor_text.text = skor.ToString();
    }

    void OnTriggerEnter2D(Collider2D temas)
    {
        if (temas.gameObject.tag == "Scorer")
        {
            skor++;

            //  Skor sesi
            AudioManager.instance.Play(AudioManager.instance.scoreSound);
        }
    }
    void OnCollisionEnter2D(Collision2D temas)
    {
        // Eğer zaten öldüyse çarpma tekrar çalışmasın
        if (isDead)
            return;

        if (temas.gameObject.tag == "pipe" || temas.gameObject.tag == "Zemin")
        {
            isDead = true; // 👈 artık öldü

            //  Çarpma sesi (sadece 1 kere)
            AudioManager.instance.Play(AudioManager.instance.hitSound);

            //  Savrulma efekti (geriye ve yukarı)
            rb.linearVelocity = new Vector2(-2f, 3f);

            //  Hafif dönme
            transform.Rotate(0, 0, -25f);

            //  Game Over ekranını aç
            GameManager.Instance.GameOver((int)skor);

            //  Ölüm anında hafif slow motion (istersen 0.3 yerine 0f da yapabilirsin)
            Time.timeScale = 0.3f;
        }
    }
}

