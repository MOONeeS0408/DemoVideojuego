

using UnityEngine;

public class ButtonAnimationController : MonoBehaviour
{
    public Animator animator; 

    // Referencia a BookManager para cambiar las páginas
    public BookManager bookManager;

    public void PlayLeftAnimation()
    {
        animator.ResetTrigger("TriggerRight");
        animator.SetTrigger("TriggerRight");

        // Cambia al estado Idle si no se está reproduciendo la animación
        animator.SetBool("AnimActivated", true);

        // Cambiar a la página anterior
        bookManager.PreviousPage();
    }

    public void PlayRightAnimation()
    {
        animator.ResetTrigger("TriggerLeft");
        animator.SetTrigger("TriggerLeft");
        
        // Cambia al estado Idle si no se está reproduciendo la animación
        animator.SetBool("AnimActivated", true); 

        // Cambiar a la siguiente página
        bookManager.NextPage();
    }
}

