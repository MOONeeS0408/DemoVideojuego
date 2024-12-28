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
            Vector3 currentPosition = transform.position;
            float newZ = Mathf.MoveTowards(currentPosition.z, targetZ, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(currentPosition.x, currentPosition.y, newZ);

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
