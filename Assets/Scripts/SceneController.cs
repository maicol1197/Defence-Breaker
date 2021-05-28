using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
   public void PressedButonStart()
    {
        SceneManager.LoadScene("Juego");
    }

    public void PressedButonQuit()
    {
        SceneManager.LoadScene("Menú Principal");
    }
}
