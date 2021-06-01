using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReFight : MonoBehaviour
{
    


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 75)
        {
            BossMovement.velocidad = 1f;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BossMovement.velocidad = -10f;
        }
    }

    
}
