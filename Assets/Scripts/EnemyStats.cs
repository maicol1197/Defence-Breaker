using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int vidaMaxima;
    public int ataque;
    public int defensa;
    public AudioClip muerteEnemigo;
    public AudioClip da�oAEnemigo;
    public AudioClip movimientoAvion = null;
    Color colorDa�o = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;
    bool recibioDa�o = false;

    TankController player;
    int vidaActual;
    Animator anim;
    AudioSource sonidoEnemigo;
    bool estaMuerto = false;
    Slider saludEnemgios;

    private void Awake()
    {
        colorNormal = this.gameObject.GetComponent<SpriteRenderer>().color; 
        vidaActual = vidaMaxima;
        anim = this.gameObject.GetComponent<Animator>();
        sonidoEnemigo = this.gameObject.GetComponent<AudioSource>();
        player = FindObjectOfType<TankController>();
        saludEnemgios = this.gameObject.GetComponentInChildren<Slider>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (recibioDa�o && !estaMuerto)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDa�o;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(this.gameObject.GetComponent<SpriteRenderer>().color, colorNormal, 5f * Time.deltaTime);
        }
        recibioDa�o = false;
        saludEnemgios.value = vidaActual;
    }

    void RecibirDa�o(int da�oRecibido)
    {
        int da�oTotal = da�oRecibido - defensa;
        if(da�oTotal > 0)
        {
            vidaActual -= da�oTotal;
        }
        sonidoEnemigo.PlayOneShot(da�oAEnemigo);
        if (vidaActual <= 0 && !estaMuerto)
        {
            Muerte();
        }
        else
        {
            recibioDa�o = true;
            sonidoEnemigo.PlayOneShot(da�oAEnemigo);
        }

    } 
        
    void Muerte()
    {   
        estaMuerto = true;

        //Cambio de animacion aca.

        sonidoEnemigo.PlayOneShot(muerteEnemigo);
        if (this.gameObject.tag == "EnemyA" || this.gameObject.tag == "EnemyT" 
            || this.gameObject.tag == "EnemyO" ) {
            EnemyManager.cantEnemigosDestruidos++;
            TankController.dinero++;
        }
        Destroy(this.gameObject,0.3f);
        
    }

    void OnTriggerEnter(Collider objeto)
    {
        if (objeto.gameObject.tag == "PlayerAtaque" && this.gameObject.tag != "TriggerA")
        {
            int ataquePlayer = player.ataque;
            RecibirDa�o(ataquePlayer);
 
        }
    }

}
