using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{
    public GameObject ExplosionDisparoPrefab;

    public Slider salud;
    public AudioClip muertePersonaje;
    public TextMeshProUGUI municionT;
    public TextMeshProUGUI dineroT;
    Animator anim;
    AudioSource sonidoPlayer;
    public Image indicadorDaño;


    public int vidaMaxima;
    public int vidaActual;
    
    
    public int defensa;
    public int ataque;
    public int municion;
    public static int dinero = 0;
    public bool estaMuerto;
    bool dañado;
    public Color colorDaño = new Color(1f, 0f, 0f, 0.1f);
    float anguloDeDisparo;
    
    
    
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        sonidoPlayer = GetComponent<AudioSource>();
        vidaActual = vidaMaxima;
        salud.maxValue = vidaMaxima;
        salud.minValue = 0;
    }
    void Start()
    {
        dinero = 0;
    }
    void Update()
    { 
       
        if (dañado && !estaMuerto)
        {
            indicadorDaño.color = colorDaño;
        }
        else
        {
            indicadorDaño.color = Color.Lerp(indicadorDaño.color, Color.clear, 5f * Time.deltaTime);
        }
        dañado = false;
        
       
        
        DisparoPersonaje();
      
        
        dineroT.text = dinero.ToString();
        municionT.text = municion.ToString();
        SeguirPuntero();
        

    }


    public void DisparoPersonaje()
    {
        if(municion > 0) { 
            if (Input.GetButtonDown("Fire1") && anguloDeDisparo <= 95 && anguloDeDisparo >= -20)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
                {
                    if (!PauseMenu.isPaused)
                    {
                        GameObject boom = Instantiate(ExplosionDisparoPrefab, worldPosition, Quaternion.identity) as GameObject;
                        boom.tag = "PlayerAtaque";
                        Destroy(boom, 0.8f);
                        municion--;
                    }
                }
                
            }
        }
    }
    public void RecibirDaño(int cantDaño)
    {
        dañado = true;
        int dañoTotal = cantDaño - this.defensa;
        if (dañoTotal > 0)
        {
            this.vidaActual -= dañoTotal;
        }
        else
        {
            this.vidaActual -= 0;
        }
        salud.value = this.vidaActual;
        sonidoPlayer.Play();
        if(this.vidaActual <= 0 && !estaMuerto)
        {
            Muerte();
        }
        
    }

    void Muerte()
    {
        estaMuerto = true;

        //Cambio de animacion aca.


        sonidoPlayer.clip = muertePersonaje;
        sonidoPlayer.Play();
        Destroy(this.gameObject, 0.5f);
        GameReset();
    }
    
    void OnTriggerEnter(Collider objeto)
    {
        if (objeto.gameObject.tag == "Enemy" || objeto.gameObject.tag == "AtaqueA" 
            || objeto.gameObject.tag == "EnemyT" || objeto.gameObject.tag == "EnemyO")
        {
            int ataqueEnemigo = objeto.gameObject.GetComponent<EnemyStats>().ataque;
            RecibirDaño(ataqueEnemigo);
            Destroy(objeto.gameObject);
        }
        if (objeto.gameObject.tag == "EnemyO" || objeto.gameObject.tag == "EnemyT")
        {
            EnemyManager.cantEnemigosDestruidos++;
        }
        if (objeto.gameObject.tag == "Ammo")
        {
            Destroy(objeto.gameObject);
            municion += 5;
        }
    }
   

    void SeguirPuntero()
    {
        Transform torreta = this.gameObject.transform.Find("Cannon1");
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(torreta.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        anguloDeDisparo = angle;
        torreta.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Clamp(angle, -20f, 95f)));
    }

    public void GameReset()
    {
        EnemyManager.cantEnemigosPorRonda = 2;
        EnemyManager.nroOleada = 1;
        EnemyManager.cantEnemigosDestruidos = 0;
        dinero = 0;
        municion = 25;
    }



}
