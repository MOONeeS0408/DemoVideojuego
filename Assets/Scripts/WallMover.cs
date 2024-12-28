using UnityEngine;

public class WallMover : MonoBehaviour
{
    public float targetZ; // Nueva posición en Z
    public float moveSpeed = 2f; // Velocidad del movimiento

    private bool shouldMove = false;

    private void Update()
    {
        if (shouldMove)
        {
            // Movemos la pared hacia la posición objetivo solo en el eje Z
            Vector3 currentPosition = transform.position;
            float newZ = Mathf.MoveTowards(currentPosition.z, targetZ, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(currentPosition.x, currentPosition.y, newZ);

            // Detenemos el movimiento si ya alcanzó la posición objetivo
            if (Mathf.Approximately(transform.position.z, targetZ))
            {
                shouldMove = false;
            }
        }
    }

    public void MoveWall()
    {
        shouldMove = true;
    }
}
