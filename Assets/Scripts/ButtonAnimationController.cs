

using UnityEngine;

public class ButtonAnimationController : MonoBehaviour
{
    public Animator animator; // Referencia al Animator

    // Referencia a BookManager para cambiar las páginas
    public BookManager bookManager;

    // Método para reproducir la animación cuando se presiona el botón de izquierda
    public void PlayLeftAnimation()
    {
        animator.ResetTrigger("TriggerRight");
        animator.SetTrigger("TriggerRight");
        // Cambia al estado Idle si no se está reproduciendo la animación
        animator.SetBool("AnimActivated", true); // Cambia el parámetro que controla Idle/Mover
        // Cambiar a la página anterior
        bookManager.PreviousPage();
    }

    // Método para reproducir la animación cuando se presiona el botón de derecha
    public void PlayRightAnimation()
    {
        // Reproducir la animación para el botón izquierdo
        animator.ResetTrigger("TriggerLeft");
        animator.SetTrigger("TriggerLeft");
        // Reproducir la animación para el botón derecho
        
        // Cambia al estado Idle si no se está reproduciendo la animación
        animator.SetBool("AnimActivated", true); // Cambia el parámetro que controla Idle/Mover
        // Cambiar a la siguiente página
        bookManager.NextPage();
    }
}

