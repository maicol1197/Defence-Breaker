using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaEnemigos : MonoBehaviour
{
    Slider saludEnemigos;

    private void Awake()
    {
        this.saludEnemigos = this.gameObject.GetComponentInChildren<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.saludEnemigos.value = this.gameObject.GetComponent<EnemyStats>().vidaActual;
    }
}
