using UnityEngine;
using TMPro; // TextMeshPro kullanıyorsan
// using UnityEngine.UI; // UI Text kullanıyorsan bu satırı aç
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject scoreUI;

    public GameObject tapToStart;   

    public TMP_Text bestScoreText;   // Game Over panelinde göstereceğiz
    private int bestScore = 0;

    public TextMeshProUGUI finalScoreText;
    // Eğer Text kullanıyorsan:
    // public Text finalScoreText;

    private bool gameStarted = false;

    public bool gameEnded = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Oyun başta durur
        Time.timeScale = 0f;

        // Panelleri ayarla
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        if (tapToStart != null)
            tapToStart.SetActive(true);   // Ana ekranda görünsün

        // Kaydedilmiş en yüksek skoru yükle
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void StartGame()
    {
        gameStarted = true;

        startPanel.SetActive(false);
        scoreUI.SetActive(true);

        if (tapToStart != null)
            tapToStart.SetActive(false);  // Oyun başlayınca yazı kaybolsun

        Time.timeScale = 1f;
    }
    public void GameOver(int score)
    {
        // Oyunun iki kez Game Over olmaması için
        if (gameEnded) return;
        gameEnded = true;

        //  Game Over sesi (Artık sadece 1 kez çalar)
        AudioManager.instance.Play(AudioManager.instance.gameOverSound);

        // Oyun dursun
        Time.timeScale = 0f;

        // Game Over panelini aç
        gameOverPanel.SetActive(true);

        // Son skoru göster
        finalScoreText.text = "Last Score: " + score;

        //  Best score güncellemesi
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        // Best skor yazdır
        bestScoreText.text = "Best Score: " + bestScore;
    }
    public void Restart()
    {
        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        // önce tık sesi çalsın
        AudioManager.instance.PlayButtonClick();

        // sesi duyman için küçük bir gecikme
        yield return new WaitForSecondsRealtime(0.1f);

        // zamanı sıfırla ve sahneyi yeniden yükle
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
