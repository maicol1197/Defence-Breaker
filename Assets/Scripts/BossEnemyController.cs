using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyController : MonoBehaviour
{
    TankController player;
    AudioSource sonidos;

    public int vidaMaxima = 100;
    public int vidaActual;
    public int defensa;
    public GameObject offsetBala;
    Slider saludTorreta;
    bool isDamaged = false;
    bool isDead = false;
    Color colorDa�o = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;

    public AudioClip muerteEnemigo;
    public AudioClip da�oAEnemigo;
    public GameObject prefabDisparo;
    void Awake()
    {
        saludTorreta = this.gameObject.GetComponentInChildren<Slider>();
        sonidos = this.gameObject.GetComponent<AudioSource>();
        vidaActual = vidaMaxima;
        saludTorreta.maxValue = vidaMaxima;
        saludTorreta.value = vidaMaxima;
        saludTorreta.minValue = 0;
        player = FindObjectOfType<TankController>();
        colorNormal = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
    void Start()
    {
    }


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
        saludTorreta.value = vidaActual;
        SeguirTank();
    }

   

    void SeguirTank()
    {
        Vector3 dir = (player.transform.position - this.gameObject.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerAtaque")
        {
            RecibirDa�o(TankController.ataque);
            sonidos.PlayOneShot(da�oAEnemigo);
        }
        if (other.gameObject.tag == "AtaqueT" && BossMovement.velocidad > 0)
        {
            StartCoroutine(Disparo());
        }
    }

    void RecibirDa�o(int da�oRecibido)
    {   

        int da�oTotal = da�oRecibido - this.defensa;
        if(da�oTotal > 0)
        {
            if (da�oTotal > this.vidaActual)
            {
                da�oTotal = vidaActual;
            }
            this.vidaActual -= da�oTotal;
            BossController.saludJefeActual -= da�oTotal;
            
        }
        isDamaged = true;

        if(this.vidaActual <= 0 && !isDead)
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



    IEnumerator Disparo()
    {
        yield return new WaitForSeconds(3);

        GameObject bala = Instantiate(prefabDisparo) as GameObject;
     
        bala.gameObject.transform.position = offsetBala.transform.position;
        
        
        Vector3 dir = (player.transform.position - bala.gameObject.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bala.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (this.gameObject != null)
        {
            StartCoroutine(Disparo());
        }
    }
   

    private void OnBecameInvisible()
    {
        this.StopAllCoroutines();
    }
}
