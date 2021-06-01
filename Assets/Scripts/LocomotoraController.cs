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
    Color colorDa�o = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;
    public AudioClip muerteEnemigo;
    public AudioClip da�oAEnemigo;

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
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDa�o;
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
            RecibirDa�o(TankController.ataque);
            sonidos.PlayOneShot(da�oAEnemigo);
        }
        
    }

    void RecibirDa�o(int da�oRecibido)
    {

        int da�oTotal = da�oRecibido - defensa;
        if (da�oTotal > 0)
        {
            if (da�oTotal > vidaActual)
            {
                da�oTotal = vidaActual;
            }
            vidaActual -= da�oTotal;
            BossController.saludJefeActual -= da�oTotal;

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
