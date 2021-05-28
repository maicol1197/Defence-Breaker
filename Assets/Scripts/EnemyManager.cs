using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    class Enemigos
    {
        public bool enUso;
        public GameObject enemy;
        public Transform t;
        public Enemigos(GameObject enemy) { 
            this.enemy = enemy;
            t = enemy.transform;
        }
        public void Usar() { enUso = true; }
        public void NoUsar() { enUso = false; }
    }

    public GameObject prefabEnemyAvion;
    public GameObject prefabEnemyTank;
    public GameObject prefabEnemyObstacle;
    public GameObject prefabBomba;
    public float SpanwPosX;


    public static int nroOleada = 1;
    public static int cantEnemigosPorRonda;
    public static bool isRoundInProgress = false;
    public static int cantEnemigosDestruidos = 0;
    Enemigos[] enemigos;


    // Start is called before the first frame update
    void Start()
    {
        RoundStart();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cantEnemigosDestruidos);
        if (!isRoundInProgress)
        {
            RoundStart();
        }

        CheckearEnemigos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TriggerA" && other.GetType() == typeof(BoxCollider))
        {
            GameObject bomba = Instantiate(prefabBomba) as GameObject;
            bomba.transform.position = new Vector3(-6f, bomba.transform.position.y, bomba.transform.position.z);
        }
        if (other.gameObject.tag == "EnemyO" || other.gameObject.tag == "EnemyT" || other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            TankController.dinero++;
        }
    }

    void RoundStart()
    {
        isRoundInProgress = true;
        ConfigurarEnemigos();
    }
    void ConfigurarEnemigos()
    {
        cantEnemigosPorRonda = 2 + nroOleada;
        enemigos = new Enemigos[cantEnemigosPorRonda];
        for (int i = 0; i < enemigos.Length; i++)
        {   
            if(i != 0)
            {
                SpanwPosX = (enemigos[i-1].t.position.x) + Random.Range(4f, 5f);
            }
            else
            {
                SpanwPosX = 15f;
            }
            switch (Random.Range(0, 3))
            {
                case 0:
                    GameObject enemyAvion = Instantiate(prefabEnemyAvion) as GameObject;
                    enemyAvion.transform.position = new Vector3(SpanwPosX, enemyAvion.transform.position.y, enemyAvion.transform.position.z);
                    enemigos[i] = new Enemigos(enemyAvion);
                    break;
                case 1:
                    GameObject enemyTank = Instantiate(prefabEnemyTank) as GameObject;
                    enemyTank.transform.position = new Vector3(SpanwPosX, enemyTank.transform.position.y, enemyTank.transform.position.z);
                    enemigos[i] = new Enemigos(enemyTank);
                    break;
                case 2:
                    GameObject enemyObstacle = Instantiate(prefabEnemyObstacle) as GameObject;
                    enemyObstacle.transform.position = new Vector3(SpanwPosX, enemyObstacle.transform.position.y, enemyObstacle.transform.position.z);
                    enemigos[i] = new Enemigos(enemyObstacle);
                    break;
                default:
                    break;
            }
        }


    }

    void RoundFinish()
    {
        isRoundInProgress = false;
        nroOleada++;
    }

    void CheckearEnemigos()
    {
        if (cantEnemigosDestruidos == cantEnemigosPorRonda)
        {
            cantEnemigosDestruidos = 0;
            RoundFinish();
        }
    }

}
