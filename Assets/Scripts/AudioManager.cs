using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;

    public AudioClip flapSound;
    public AudioClip scoreSound;
    public AudioClip hitSound;
    public AudioClip gameOverSound;

    public AudioSource uiAudioSource;  // tık sesi için ayrı kaynak
    public AudioClip buttonClickSound;  // buton tık sesi
    public Image soundButtonImage;

    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    //  Ses açık mı kapalı mı?
    public bool isMuted = false;
    // (İstersen butonun üstündeki yazıyı buraya bağlayacağız)
    public TMP_Text soundButtonText;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // Oyun ilk açıldığında PlayerPrefs'ten oku (varsayılan ses açık)
        int soundOn = PlayerPrefs.GetInt("soundOn", 1); // 1 = açık, 0 = kapalı
        isMuted = (soundOn == 0);
        UpdateSoundState();
    }

    // Ses çalma fonksiyonu
    public void Play(AudioClip clip)
    {
        if (clip == null || isMuted) return;

        audioSource.PlayOneShot(clip);
    }
    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
            uiAudioSource.PlayOneShot(buttonClickSound);
    }
    // BUTONUN ÇAĞIRACAĞI FONKSİYON
    public void ToggleSound()
    {
        isMuted = !isMuted;

        // Kaydet (sonraki açılışta hatırlasın)
        PlayerPrefs.SetInt("soundOn", isMuted ? 0 : 1);

        UpdateSoundState();
    }
    // Ses durumuna göre AudioSource ve buton yazısını günceller
    void UpdateSoundState()
    {
        if (audioSource != null)
            audioSource.mute = isMuted;

        if (soundButtonImage != null)
            soundButtonImage.sprite = isMuted ? soundOffIcon : soundOnIcon;
    }
}
