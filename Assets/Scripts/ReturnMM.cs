using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMM : MonoBehaviour
{
    [SerializeField] private float delay = 5f; // Tiempo en segundos antes de regresar al menú principal

    private void Start()
    {
        // Inicia la cuenta regresiva para cambiar de escena
        Invoke("LoadMainMenu", delay);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Cambia el nombre de la escena al de tu menú principal
    }
}
