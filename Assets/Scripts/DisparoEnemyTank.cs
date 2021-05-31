using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemyTank : MonoBehaviour
{

    public GameObject prefabDisparo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AtaqueT")
        {
            StartCoroutine(Disparo());
        }
    }

    IEnumerator Disparo()
    {
        yield return new WaitForSeconds(3);
        
        GameObject bala = Instantiate(prefabDisparo) as GameObject;
        bala.transform.position = new Vector3(this.gameObject.transform.position.x - 1.20f, this.gameObject.transform.position.y + 0.25f, 0);
        if (this.gameObject != null)
        {
            StartCoroutine(Disparo());
        }
    }
}
