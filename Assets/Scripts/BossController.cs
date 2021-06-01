using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject prefabLocomotora;
    public GameObject prefabGris;
    public GameObject prefabAzul;
    public GameObject prefabRojo;
    public GameObject prefabVerde;
    GameObject Locomotora;
    GameObject VagonGris;
    GameObject VagonAzul;
    GameObject VagonVerde;
    GameObject VagonRojo;

    public static bool isBossAlive = false;
    bool leerPartesDelJefe = false;
    public Slider saludJefe;
    TankController player;
    GameObject vias;
    GameObject[] partesDelJefe;



    public static int saludJefeActual;
    int saludJefeMaxima = 0;
    

    private void Awake()
    {
        vias = GameObject.Find("TerrenoVias").gameObject;
        vias.SetActive(false);
        player = FindObjectOfType<TankController>();
        saludJefe.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(EnemyManager.nroOleada%5 == 0 && !isBossAlive)
        {
           
            saludJefe.gameObject.SetActive(true);
            vias.GetComponent<Parallax>().despawnPos = -20;
            vias.SetActive(true);
            SpawnearBoss();
            if (!leerPartesDelJefe)
            {
                GameObject[] partesDelJefe = GameObject.FindGameObjectsWithTag("EnemyBossP");
                Debug.Log("Lista: " + partesDelJefe.Length);
                foreach (GameObject go in partesDelJefe)
                {
                    if (go.gameObject.GetComponent<BossEnemyController>())
                    {
                        saludJefeMaxima += go.gameObject.GetComponent<BossEnemyController>().vidaMaxima;
                    }
                    else
                    {
                        saludJefeMaxima += go.gameObject.GetComponent<LocomotoraController>().vidaMaxima;
                    }
                }
                saludJefeActual = saludJefeMaxima;
                saludJefe.maxValue = saludJefeMaxima;
                saludJefe.value = saludJefeMaxima;
                leerPartesDelJefe = true;
            }
        }

        saludJefe.value = saludJefeActual;
        if (saludJefeActual <= 0 && isBossAlive)
        {
            Muerte();
        }
    }

    void SpawnearBoss()
    {
        isBossAlive = true;
        Locomotora = Instantiate(prefabLocomotora) as GameObject;
        Locomotora.transform.position = new Vector3(71.06f, 1.4f, 0);
        VagonGris = Instantiate(prefabGris) as GameObject;
        VagonGris.transform.position = new Vector3(55.52f, 0.75f, 0);
        VagonAzul = Instantiate(prefabAzul) as GameObject;
        VagonAzul.transform.position = new Vector3(42.84f, 0.77f, 0);
        VagonVerde = Instantiate(prefabVerde) as GameObject;
        VagonVerde.transform.position = new Vector3(30.09f,0.8f, 0);
        VagonRojo = Instantiate(prefabRojo) as GameObject;
        VagonRojo.transform.position = new Vector3(16.72f,0.78f, 0);

    }


    void Muerte()
    {
        isBossAlive = false;
        Destroy(Locomotora,0.8f);
        Destroy(VagonGris, 0.8f);
        Destroy(VagonAzul, 0.8f);
        Destroy(VagonVerde, 0.8f);
        Destroy(VagonRojo, 0.8f);
        vias.GetComponent<Parallax>().despawnPos = 20;
        vias.SetActive(false);
        saludJefe.gameObject.SetActive(false);
        EnemyManager.nroOleada++;
        EnemyManager.cantEnemigosDestruidos = 0;
    }

    
}
