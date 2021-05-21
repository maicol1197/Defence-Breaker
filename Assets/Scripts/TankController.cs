using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankController : MonoBehaviour
{
    public GameObject ExplosionDisparoPrefab;
    public GameObject menuPrincipal;
    public GameObject menuOpciones;

    public Slider Salud;
    public TextMeshProUGUI municionT;
    public TextMeshProUGUI dineroT;



    public int vida;
    public float defensa;
    public float ataque;
    public int municion;
    public int dinero;
    
    void Start()
    {
        
    }
    void Update()
    {
        if (!menuPrincipal.activeSelf && !menuOpciones.activeSelf) {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
                {
                    GameObject boom = Instantiate(ExplosionDisparoPrefab, worldPosition, Quaternion.identity) as GameObject;
                    Destroy(boom, 0.9f);
                }
            }
        }

        Salud.value = vida;
        dineroT.text = dinero.ToString();
        municionT.text = municion.ToString();

    }


    


}
