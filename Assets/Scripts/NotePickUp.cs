using System.Collections;
using TMPro;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [SerializeField] private Material pageMaterial;  
    [SerializeField] private float pickupRange = 5f; 
    [SerializeField] private GameObject pickupText;

    [SerializeField] private TMP_Text messageText;  
    [SerializeField] private string noteName = "Nota importante"; // Nombre de la nota 

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

        gameObject.SetActive(false);

        //Destroy(gameObject);
        messageText.text = $"Se ha agregado \"{noteName}\"  a la libreta de notas. Checa la libreta de notas con la tecla (N).";
        messageText.gameObject.SetActive(true);
        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }

        if (pageMaterial != null)
        {
            BookManager.Instance.AddPageMaterial(pageMaterial);
        }

        Invoke(nameof(FinishNotePickup), 5f);
    }

    private void FinishNotePickup()
    {
 
        messageText.text = "";
        messageText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
