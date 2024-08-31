using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // GameOverPanel'i ba�lamak i�in
    [SerializeField] private TouchSlider touchSlider; // TouchSlider referans�
    [SerializeField] private GameObject pausePanel; // PausePanel'i ba�lamak i�in
    [SerializeField] private GameObject pauseButton; // PauseButton referans�

    private void Start()
    {
        // Ba�lang��ta paneli kapal� tut
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        touchSlider.enabled = true; // Ba�lang��ta touchSlider etkin
        pauseButton.SetActive(true); // Ba�lang��ta pauseButton etkin
    }

    public void GameOver()
    {
        // Paneli aktif et
        gameOverPanel.SetActive(true);
        touchSlider.enabled = false; // Game over oldu�unda touchSlider'� devre d��� b�rak
    }

    public void LoadMainMenu()
    {
        // Ana men� sahnesini y�kle
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        // Pause panelini a� ve oyunu durdur
        pausePanel.SetActive(true);
        Time.timeScale = 0; // Oyunu durdur
        touchSlider.enabled = false; // TouchSlider'� devre d��� b�rak
    }

    public void ResumeGame()
    {
        // Pause panelini kapat ve oyunu devam ettir
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Oyunu devam ettir
        touchSlider.enabled = true; // TouchSlider'� etkinle�tir
    }

    public void ConfirmExitToMainMenu()
    {
        // Pause panelindeki 'Yes' butonuna bas�ld���nda ana men�ye d�n
        Time.timeScale = 1; // Zaman� s�f�rdan ��karmay� unutmay�n
        SceneManager.LoadScene("MainMenu");
    }
}
