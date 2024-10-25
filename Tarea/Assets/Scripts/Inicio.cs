using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    // Método para cambiar de escena
    public void CambiarEscena()
    {
        SceneManager.LoadScene(1); // Cambia '1' por el índice de la escena deseada
    }
}
