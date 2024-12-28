using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [SerializeField] private Material pageMaterial;  // Material que contiene la textura de la página
    [SerializeField] private float pickupRange = 5f; // Rango para recoger la nota
    [SerializeField] private GameObject pickupText; 
    private bool isTaken = false;
    private Transform playerTransform;

    private void Start()
    {
       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró al jugador en la escena. Asegúrate de que el objeto del jugador tenga la etiqueta 'Player'.");
        }

        
        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }
    }

    private void Update()
    {
        if (isTaken || playerTransform == null) return;

        // Calcula la distancia entre el jugador y la nota
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // Si el jugador está dentro del rango, activa el texto
        if (distanceToPlayer <= pickupRange && Input.GetKeyDown(KeyCode.F))
        {
            
                TakeNote();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            pickupText.SetActive(true);
        }
    }

  
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            pickupText.SetActive(false);
        }
    }

    private void TakeNote()
    {
        isTaken = true;

       
        Destroy(gameObject); 
        
        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }

        if (pageMaterial != null)
        {
            BookManager.Instance.AddPageMaterial(pageMaterial);
        }
    }
}
