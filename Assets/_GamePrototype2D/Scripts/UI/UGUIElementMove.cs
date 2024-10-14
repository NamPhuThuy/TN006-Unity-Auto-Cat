using UnityEngine;

public class UGUIElementMove : MonoBehaviour
{
    private enum MoveType
    {
        HORIZONTAL,
        VERTICAL,
        DIAGONAL,
        REVERSE_DIAGONAL
    }
    
    [SerializeField] private float speed = 5f; // Speed of movement
    [SerializeField] private float movementRange = 50f; // How far the image will move up and down
    [SerializeField] private MoveType moveType;
    
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position; // Store the image's starting position
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * speed) * movementRange; // Use sine function for smooth movement

        if (moveType == MoveType.VERTICAL)
        {
            transform.position = startingPosition + new Vector3(0, movement, 0); // Update y position based on movement
        }
        else if (moveType == MoveType.HORIZONTAL)
        {
            transform.position = startingPosition + new Vector3(movement, 0, 0); // Update y position based on movement
        }
        else if (moveType == MoveType.DIAGONAL)
        {
            Vector3 moveDis = new Vector3(movement, movement, 0f) / Mathf.Sqrt(2);
            
            transform.position = startingPosition + moveDis; // Update y position based on movement
        }
        else if (moveType == MoveType.REVERSE_DIAGONAL)
        {
            Vector3 moveDis = new Vector3(-movement, movement, 0f) / Mathf.Sqrt(2);
            
            transform.position = startingPosition + moveDis; // Update y position based on movement
        }
    }
}