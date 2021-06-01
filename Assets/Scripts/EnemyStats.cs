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
        if (recibioDa�o && !estaMuerto)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDa�o;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(this.gameObject.GetComponent<SpriteRenderer>().color, colorNormal, 5f * Time.deltaTime);
        }
        recibioDa�o = false;
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
            RecibirDa�o(ataquePlayer);
 
        }
    }

}
