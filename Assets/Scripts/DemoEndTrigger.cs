using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambia a la escena Fin de Demo
            SceneManager.LoadScene("EndDemo");
        }
    }
}