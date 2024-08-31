using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text totalScoreText; // Toplam skoru göstermek için
    [SerializeField] private TMP_Text addedScoreText; // Eklenen skoru göstermek için
    [SerializeField] private TMP_Text messageText;    // Skor mesajlarýný göstermek için

    private AudioSource audioSource;

    private int totalScore = 0;
    private int highScore = 0; // Yüksek skoru takip etmek için
    private int[] scoreThresholds = { 1000, 2000, 4000, 8000, 16000, 32000 };
    private string[] messages = { "Good!", "Nice!", "Amazing!", "Awesome!", "Incredible!", "Impossible!" };
    private int nextThresholdIndex = 0;

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource bileþenini al
        audioSource = GetComponent<AudioSource>();

        // Yüksek skoru yükle
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreText.text = "" + totalScore;

        addedScoreText.text = "+" + score;

        // Ses dosyasýný çal
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Yüksek skoru güncelle
        if (totalScore > highScore)
        {
            highScore = totalScore;
            PlayerPrefs.SetInt("HighScore", highScore); // Yüksek skoru kaydet
        }

        // Bir süre sonra eklenen skoru gizleyelim
        Invoke("HideAddedScore", 1.5f);

        CheckForScoreThreshold();
    }

    private void HideAddedScore()
    {
        addedScoreText.text = "";
    }

    private void CheckForScoreThreshold()
    {
        if (nextThresholdIndex < scoreThresholds.Length && totalScore >= scoreThresholds[nextThresholdIndex])
        {
            // Mesajý göster
            ShowMessage(messages[nextThresholdIndex]);
            nextThresholdIndex++;
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        // Mesajý 2 saniye sonra gizleyelim
        Invoke("HideMessage", 2f);
    }

    private void HideMessage()
    {
        messageText.text = "";
    }

    // Yüksek skoru döndüren bir metod ekleyelim
    public int GetHighScore()
    {
        return highScore;
    }
}