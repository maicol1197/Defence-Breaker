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
    public Image indicadorDaņo;


    public static int vidaMaxima = 100;
    public static int vidaActual;
    
    
    public static int defensa = 10;
    public static int ataque = 50;
    public static int municion = 30;
    public static int dinero = 0;
    public bool estaMuerto;
    bool daņado;
    public Color colorDaņo = new Color(1f, 0f, 0f, 0.1f);
    float anguloDeDisparo;
    
    
    
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        sonidoPlayer = GetComponent<AudioSource>();
        vidaActual = vidaMaxima;
        salud.minValue = 0;
    }
    void Start()
    {
        dinero = 0;
    }
    void Update()
    {
        salud.maxValue = vidaMaxima;
        salud.value = vidaActual;
        if (daņado && !estaMuerto)
        {
            indicadorDaņo.color = colorDaņo;
        }
        else
        {
            indicadorDaņo.color = Color.Lerp(indicadorDaņo.color, Color.clear, 5f * Time.deltaTime);
        }
        daņado = false;
        
       
        
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
    public void RecibirDaņo(int cantDaņo)
    {
        daņado = true;
        int daņoTotal = cantDaņo - defensa;
        if (daņoTotal > 0)
        {
            vidaActual -= daņoTotal;
        }
        else
        {
            vidaActual -= 0;
        }
        sonidoPlayer.Play();
        if(vidaActual <= 0 && !estaMuerto)
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
            RecibirDaņo(ataqueEnemigo);
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
        if (objeto.gameObject.tag == "Heal")
        {
            Destroy(objeto.gameObject);
            if(vidaActual + 15 > vidaMaxima)
            {
                vidaActual = vidaMaxima;
            }
            else
            {
                vidaActual += 15;
            }
            
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
