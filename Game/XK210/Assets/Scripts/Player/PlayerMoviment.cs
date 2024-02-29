
// Player Movement script
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    // Movement speed
    public float MovementSpeed;

    public PlayerMoviment(float movementSpeed)
    {
        MovementSpeed = movementSpeed;
    }
    {
        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    // Create a Vector3 for the movement direction
    Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput) * MovementSpeed * Time.deltaTime;

    // Move the player
    transform.position += movementDirection;
    }
}