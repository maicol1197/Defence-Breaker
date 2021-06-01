using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0.1f)
        {
            time += 1 * Time.deltaTime;
        }
        else
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
       
    }
}
