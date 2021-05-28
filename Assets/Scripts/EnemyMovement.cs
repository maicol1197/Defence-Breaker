using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float velocidad;
    // Start is called before the first frame update

    void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;
        if(this.gameObject.transform.position.x <= -12)
        {
            Destroy(this.gameObject);
            EnemyManager.cantEnemigosDestruidos++;
        }
    }
}
