using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [SerializeField] private Material pageMaterial;  // Material que contiene la textura de la página
    [SerializeField] private float pickupRange = 5f; // Rango en el que el jugador puede recoger la nota
    [SerializeField] private GameObject pickupText; // Referencia al texto que se muestra cuando está cerca de la nota
    private bool isTaken = false;
    private Transform playerTransform;

    private void Start()
    {
        // Encuentra al jugador en la escena
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró al jugador en la escena. Asegúrate de que el objeto del jugador tenga la etiqueta 'Player'.");
        }

        // Desactiva el texto inicialmente
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
        // Si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Muestra el texto indicando que se puede recoger la nota
            pickupText.SetActive(true);
        }
    }

    // Llamado cuando otro collider sale del trigger
    private void OnTriggerExit(Collider other)
    {
        // Si el objeto que sale del trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Oculta el texto cuando el jugador se aleja
            pickupText.SetActive(false);
        }
    }

    private void TakeNote()
    {
        isTaken = true;

        // Desactiva la nota en el mundo (el objeto de la nota se desactiva)
        Destroy(gameObject); // Elimina el objeto del mundo
        // Desactiva el texto también
        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }

        // Si se tiene un material, se agrega al BookManager
        if (pageMaterial != null)
        {
            BookManager.Instance.AddPageMaterial(pageMaterial);
        }
    }
}
