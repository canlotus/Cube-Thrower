using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text totalScoreText; // Toplam skoru g�stermek i�in
    [SerializeField] private TMP_Text addedScoreText; // Eklenen skoru g�stermek i�in
    [SerializeField] private TMP_Text messageText;    // Skor mesajlar�n� g�stermek i�in

    private AudioSource audioSource;

    private int totalScore = 0;
    private int highScore = 0; // Y�ksek skoru takip etmek i�in
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

        // AudioSource bile�enini al
        audioSource = GetComponent<AudioSource>();

        // Y�ksek skoru y�kle
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreText.text = "" + totalScore;

        addedScoreText.text = "+" + score;

        // Ses dosyas�n� �al
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Y�ksek skoru g�ncelle
        if (totalScore > highScore)
        {
            highScore = totalScore;
            PlayerPrefs.SetInt("HighScore", highScore); // Y�ksek skoru kaydet
        }

        // Bir s�re sonra eklenen skoru gizleyelim
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
            // Mesaj� g�ster
            ShowMessage(messages[nextThresholdIndex]);
            nextThresholdIndex++;
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        // Mesaj� 2 saniye sonra gizleyelim
        Invoke("HideMessage", 2f);
    }

    private void HideMessage()
    {
        messageText.text = "";
    }

    // Y�ksek skoru d�nd�ren bir metod ekleyelim
    public int GetHighScore()
    {
        return highScore;
    }
}