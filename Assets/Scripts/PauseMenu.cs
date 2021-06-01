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
    public TextMeshProUGUI mostrarPuntos;
    public TextMeshProUGUI mostrarHighScore;
    public static int puntosActuales = 0;
    public Button botonPausa;
    Color color;
    GameObject botonMejoras;
    GameObject panelMejoras;

    TankController player;

    private void Awake()
    {

        panelMejoras = GameObject.Find("MenuDeMejoras");
        botonMejoras = GameObject.Find("DesplegarMejoras");
        color = indicadorOleada.color;
        player = FindObjectOfType<TankController>();
        JuegoResumido();

    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.nroOleada - 10 < 0)
        {
            indicadorOleada.text = "Wave: 0" + EnemyManager.nroOleada;
        }
        else
        {
            indicadorOleada.text = "Wave: " + EnemyManager.nroOleada;
        }
        if (!EnemyManager.isRoundInProgress && !BossController.isBossAlive)
        {
            color = new Color(255, 255, 255, 255);
        }
        color = Color.Lerp(color, Color.clear, 0.85f * Time.deltaTime);
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
        Time.timeScale = 0f;
        isPaused = true;
        isOptionsOpen = true;
    }

    public void OpcionesCerradas()
    {
        pauseMenu.SetActive(true);
        opciones.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        isOptionsOpen = false;
    }

    public void PressedButonQuit()
    {
        JuegoResumido();
        player.GameReset();
        SceneManager.LoadScene("Menú Principal");
    }

    public void GameOverScreen(bool muerto)
    {
        if (muerto)
        {
            botonMejoras.SetActive(false);
            panelMejoras.SetActive(false);
            gameOverMenu.SetActive(true);
            mostrarPuntos.text = puntosActuales.ToString();
            if (puntosActuales > PlayerPrefs.GetInt("HScoreNro", 0))
            {
                PlayerPrefs.SetInt("HScoreNro", puntosActuales);
            }
            mostrarHighScore.text = PlayerPrefs.GetInt("HScoreNro", 0).ToString();
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void Reiniciar()
    {
        JuegoResumido();
        player.GameReset();
        puntosActuales = 0;
        SceneManager.LoadScene("Juego");
    }

}

