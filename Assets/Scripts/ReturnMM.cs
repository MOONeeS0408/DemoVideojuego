using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMM : MonoBehaviour
{
    [SerializeField] private float delay = 5f; 
    private void Start()
    {
        
        Invoke("LoadMainMenu", delay);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
