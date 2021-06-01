using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MejorasController : MonoBehaviour
{
    public TextMeshProUGUI ataqueTxt;
    public TextMeshProUGUI defensaTxt;
    public TextMeshProUGUI vidaMaximaTxt;
    public TextMeshProUGUI municionTxt;
    GameObject menu;


    void Start()
    {
        menu = GameObject.Find("MenuDeMejoras");
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ataqueTxt.text = "Attack: " +TankController.ataque.ToString();
        defensaTxt.text = "Defense: " + TankController.defensa.ToString();
        vidaMaximaTxt.text = "Max HP: " + TankController.vidaMaxima.ToString();
        municionTxt.text = "Ammo: " + TankController.municion.ToString();
    }

    public void ComprarAtaque()
    {
        if (TankController.dinero >= 10)
        {
            TankController.ataque += 10;
            TankController.dinero -= 10;
        }
            
    }
    public void ComprarDefensa()
    {
        if (TankController.dinero >= 10)
        {
            TankController.defensa += 2;
            TankController.dinero -= 10;
        }
    }
    public void ComprarVida()
    {
        if (TankController.dinero >= 10)
        {
            TankController.vidaMaxima += 25;
            TankController.vidaActual += 25;
            TankController.dinero -= 10;
        }
        
    }
    public void ComprarMunicion()
    {
        if (TankController.dinero >= 5)
        {
            TankController.municion += 5;
            TankController.dinero -= 5;
        }
    }

    public void DesplegarMenu()
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }
}
