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
    Slider saludTorreta;
    bool isDamaged = false;
    bool isDead = false;
    Color colorDaņo = new Color(255f, 0f, 0f, 0.1f);
    Color colorNormal;

    public AudioClip muerteEnemigo;
    public AudioClip daņoAEnemigo;
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
            this.gameObject.GetComponent<SpriteRenderer>().color = colorDaņo;
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
            RecibirDaņo(player.ataque);
            sonidos.PlayOneShot(daņoAEnemigo);
        }
        if (other.gameObject.tag == "AtaqueT")
        {
            StartCoroutine(Disparo());
        }
    }

    void RecibirDaņo(int daņoRecibido)
    {   

        int daņoTotal = daņoRecibido - this.defensa;
        if(daņoTotal > 0)
        {
            if (daņoTotal > this.vidaActual)
            {
                daņoTotal = vidaActual;
            }
            this.vidaActual -= daņoTotal;
            BossController.saludJefeActual -= daņoTotal;
            
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
        Destroy(this.gameObject, 0.5f);
    }



    IEnumerator Disparo()
    {
        yield return new WaitForSeconds(3);

        GameObject bala = Instantiate(prefabDisparo) as GameObject;
        bala.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 1.20f, this.gameObject.transform.position.y + 0.25f, 0);
        Vector3 dir = (player.transform.position - bala.gameObject.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bala.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (this.gameObject != null)
        {
            StartCoroutine(Disparo());
        }
    }
}
