using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // GameOverPanel'i baðlamak için
    [SerializeField] private TouchSlider touchSlider; // TouchSlider referansý
    [SerializeField] private GameObject pausePanel; // PausePanel'i baðlamak için
    [SerializeField] private GameObject pauseButton; // PauseButton referansý

    private void Start()
    {
        // Baþlangýçta paneli kapalý tut
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        touchSlider.enabled = true; // Baþlangýçta touchSlider etkin
        pauseButton.SetActive(true); // Baþlangýçta pauseButton etkin
    }

    public void GameOver()
    {
        // Paneli aktif et
        gameOverPanel.SetActive(true);
        touchSlider.enabled = false; // Game over olduðunda touchSlider'ý devre dýþý býrak
    }

    public void LoadMainMenu()
    {
        // Ana menü sahnesini yükle
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        // Pause panelini aç ve oyunu durdur
        pausePanel.SetActive(true);
        Time.timeScale = 0; // Oyunu durdur
        touchSlider.enabled = false; // TouchSlider'ý devre dýþý býrak
    }

    public void ResumeGame()
    {
        // Pause panelini kapat ve oyunu devam ettir
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Oyunu devam ettir
        touchSlider.enabled = true; // TouchSlider'ý etkinleþtir
    }

    public void ConfirmExitToMainMenu()
    {
        // Pause panelindeki 'Yes' butonuna basýldýðýnda ana menüye dön
        Time.timeScale = 1; // Zamaný sýfýrdan çýkarmayý unutmayýn
        SceneManager.LoadScene("MainMenu");
    }
}
