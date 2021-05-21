using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OpcionesManager : MonoBehaviour
{
    Resolution[] resoluciones;
    public Toggle toggle;
    public TMP_Dropdown resolucionesLista;
    List<ResolucionesDisponibles> ressDisp = new List<ResolucionesDisponibles>();

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesLista.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionesActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            if (resoluciones[i].width >= 1024)
            {
                
                string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
                string opcionHz = " : " + resoluciones[i].refreshRate + "hz";

                ResolucionesDisponibles resDisp = new ResolucionesDisponibles(opcion,i,opcionHz);
                ressDisp.Add(resDisp);
                opciones.Add(opcion + opcionHz);
                if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width
                    && resoluciones[i].height == Screen.currentResolution.height)
                {
                    resolucionesActual = resDisp.Indice;
                }
            }
        }

        resolucionesLista.AddOptions(opciones);
        resolucionesLista.value = resolucionesActual;
        resolucionesLista.RefreshShownValue();
    }

    public void PantallaCompleta(bool pantalla)
    {
        Screen.fullScreen = pantalla;
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        for (int i = 0; i < ressDisp.Count; i++)
        {
            if (resolucionesLista.options[indiceResolucion].text.Contains(ressDisp[i].Name))
            {
                Resolution resolucion = resoluciones[ressDisp[i].Indice];
                Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
            }
        }
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}

public struct ResolucionesDisponibles
{
    string name;
    int indice;
    string refresh;

    public ResolucionesDisponibles(string _name, int _indice, string _refresh)
    {
        name = _name;
        indice = _indice;
        refresh = _refresh;
    }

    public string Name
    {
        get { return name; }
    }

    public int Indice
    {
        get { return indice; }
    }

    public string Refresh
    {
        get { return refresh; }
    }
}

