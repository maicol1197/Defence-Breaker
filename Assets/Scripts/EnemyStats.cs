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
    public AudioClip daņoAEnemigo;
    public AudioClip movimientoAvion = null;
    Color colorDaņo = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;
    bool recibioDaņo = false;

    TankController player;
    public int vidaActual;
    Animator anim;
    AudioSource sonidoEnemigo;
    bool estaMuerto = false;
    Slider saludEnemgios;
    EnemyManager enemyManager;

    private void Awake()
    {
        colorNormal = this.gameObject.GetComponent<SpriteRenderer>().color; 
        vidaActual = vidaMaxima;
        anim = this.gameObject.GetComponent<Animator>();
        sonidoEnemigo = this.gameObject.GetComponent<AudioSource>();
        player = FindObjectOfType<TankController>();
        enemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
       
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (recibioDaņo && !estaMuerto)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDaņo;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(this.gameObject.GetComponent<SpriteRenderer>().color, colorNormal, 5f * Time.deltaTime);
        }
        recibioDaņo = false;
    }

    void RecibirDaņo(int daņoRecibido)
    {
        int daņoTotal = daņoRecibido - defensa;
        if(daņoTotal > 0)
        {
            vidaActual -= daņoTotal;
        }
        sonidoEnemigo.PlayOneShot(daņoAEnemigo);
        if (vidaActual <= 0 && !estaMuerto)
        {
            Muerte();
        }
        else
        {
            recibioDaņo = true;
            sonidoEnemigo.PlayOneShot(daņoAEnemigo);
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
            PauseMenu.puntosActuales++;
        }
        Destroy(this.gameObject,0.3f);
        
        if(Random.Range(0,100)+1 <= 25 && this.gameObject.tag != "Enemy" && this.gameObject.tag != "AtaqueA")
        {
            GameObject ammo = Instantiate(enemyManager.ammoBox, this.transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider objeto)
    {
        if (objeto.gameObject.tag == "PlayerAtaque" && this.gameObject.tag != "TriggerA")
        {
            int ataquePlayer = TankController.ataque;
            RecibirDaņo(ataquePlayer);
 
        }
    }

}
