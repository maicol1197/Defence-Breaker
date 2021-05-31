using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackMovement : MonoBehaviour
{
    GameObject player;
    public float velocidad;

    private void Awake()
    {
        player = FindObjectOfType<TankController>().gameObject;
    }
    void Start()
    {
        
    }

    void Update()
    {
        this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, player.gameObject.transform.position, velocidad * Time.deltaTime);
    }
}