using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isOptionsOpen = false;
    public GameObject pauseMenu;
    public GameObject opciones;
    public GameObject gameOverMenu;
    public GameObject panelOleadas;
    public TextMeshProUGUI indicadorOleada;
    public Button botonPausa;
    Color color;


    TankController player;

    private void Awake()
    {
        color = indicadorOleada.color;
        player = FindObjectOfType<TankController>();
        JuegoResumido();
    }
    // Update is called once per frame
    void Update()
    {
        indicadorOleada.text = "Wave: 0" + EnemyManager.nroOleada;
        if (!EnemyManager.isRoundInProgress)
        {
            color = new Color(255, 255, 255, 255);
        }
        color = Color.Lerp(color, Color.clear, 0.7f * Time.deltaTime);
        indicadorOleada.color = color;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            JuegoPausado();
        }
        GameOverScreen(player.estaMuerto);
        
    }

    public void JuegoPausado()
    {
        if (isOptionsOpen)
        {
            OpcionesCerradas();
        }
        else
        {
            if (isPaused)
            {
                JuegoResumido();
            }
            else
            {
                panelOleadas.SetActive(false);
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
        
    }


    public void JuegoResumido()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        panelOleadas.SetActive(true);
    }
    public void OpcionesAbiertas()
    {
        pauseMenu.SetActive(false);
        opciones.SetActive(true);
        isPaused = true;
        isOptionsOpen = true;
    }

    public void OpcionesCerradas()
    {
        pauseMenu.SetActive(true);
        opciones.SetActive(false);
        isPaused = true;
        isOptionsOpen = false;
    }

    public void PressedButonQuit()
    {
        JuegoResumido();
        SceneManager.LoadScene("Menú Principal");
    }

    public void GameOverScreen(bool muerto)
    {
        if (muerto)
        {
            Time.timeScale = 0f;
            isPaused = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Reiniciar()
    {
        JuegoResumido();
        SceneManager.LoadScene("Juego");
    }

}

