using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocomotoraController : MonoBehaviour
{
    public int vidaMaxima = 200;
    public static int vidaActual;
    public static int defensa = 15;
    Slider saludSlider;
    bool isDamaged = false;
    bool isDead = false;
    Color colorDaņo = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;
    public AudioClip muerteEnemigo;
    public AudioClip daņoAEnemigo;

    AudioSource sonidos;
    TankController player;


    private void Awake()
    {
        saludSlider = this.gameObject.GetComponentInChildren<Slider>();
        sonidos = this.gameObject.GetComponent<AudioSource>();
        vidaActual = vidaMaxima;
        saludSlider.maxValue = vidaMaxima;
        saludSlider.value = vidaMaxima;
        saludSlider.minValue = 0;
        player = FindObjectOfType<TankController>();
        colorNormal = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaged && !isDead)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDaņo;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(this.gameObject.GetComponent<SpriteRenderer>().color, colorNormal, 5f * Time.deltaTime);
        }
        isDamaged = false;
        saludSlider.value = vidaActual;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerAtaque")
        {
            RecibirDaņo(TankController.ataque);
            sonidos.PlayOneShot(daņoAEnemigo);
        }
        
    }

    void RecibirDaņo(int daņoRecibido)
    {

        int daņoTotal = daņoRecibido - defensa;
        if (daņoTotal > 0)
        {
            if (daņoTotal > vidaActual)
            {
                daņoTotal = vidaActual;
            }
            vidaActual -= daņoTotal;
            BossController.saludJefeActual -= daņoTotal;

        }
        isDamaged = true;

        if (vidaActual <= 0 && !isDead)
        {
            Muerto();
        }

    }
    void Muerto()
    {
        isDead = true;
        sonidos.PlayOneShot(muerteEnemigo);
        Destroy(this.gameObject, 0.8f);
        TankController.dinero += 5;
    }
}
