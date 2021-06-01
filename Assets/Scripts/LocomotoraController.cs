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
    Color colorDaño = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;
    public AudioClip muerteEnemigo;
    public AudioClip dañoAEnemigo;

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
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDaño;
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
            RecibirDaño(TankController.ataque);
            sonidos.PlayOneShot(dañoAEnemigo);
        }
        
    }

    void RecibirDaño(int dañoRecibido)
    {

        int dañoTotal = dañoRecibido - defensa;
        if (dañoTotal > 0)
        {
            if (dañoTotal > vidaActual)
            {
                dañoTotal = vidaActual;
            }
            vidaActual -= dañoTotal;
            BossController.saludJefeActual -= dañoTotal;

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
