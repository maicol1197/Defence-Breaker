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
    public AudioClip dañoAEnemigo;
    public AudioClip movimientoAvion = null;
    Color colorDaño = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;
    bool recibioDaño = false;

    TankController player;
    public int vidaActual;
    Animator anim;
    AudioSource sonidoEnemigo;
    bool estaMuerto = false;
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
        if (recibioDaño && !estaMuerto)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDaño;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(this.gameObject.GetComponent<SpriteRenderer>().color, colorNormal, 5f * Time.deltaTime);
        }
        recibioDaño = false;
    }

    void RecibirDaño(int dañoRecibido)
    {
        int dañoTotal = dañoRecibido - defensa;
        if(dañoTotal > 0)
        {
            vidaActual -= dañoTotal;
        }
        sonidoEnemigo.PlayOneShot(dañoAEnemigo);
        if (vidaActual <= 0 && !estaMuerto)
        {
            Muerte();
        }
        else
        {
            recibioDaño = true;
            sonidoEnemigo.PlayOneShot(dañoAEnemigo);
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
        int randomNumber = Random.Range(0, 100) + 1;
        if (randomNumber <= 25 && this.gameObject.tag != "Enemy" && this.gameObject.tag != "AtaqueA")
        {
            GameObject ammo = Instantiate(enemyManager.ammoBox, this.transform.position, Quaternion.identity);
        }else if(randomNumber >= 70 && randomNumber <= 100 && this.gameObject.tag != "Enemy" && this.gameObject.tag != "AtaqueA")
        {
            GameObject tools = Instantiate(enemyManager.toolKit, this.transform.position, Quaternion.identity);
        }

    }

    void OnTriggerEnter(Collider objeto)
    {
        if (objeto.gameObject.tag == "PlayerAtaque" && this.gameObject.tag != "TriggerA")
        {
            int ataquePlayer = TankController.ataque;
            RecibirDaño(ataquePlayer);
 
        }
    }

}
