using UnityEngine;
using Unity.Netcode;

public class MovingObstacle : NetworkBehaviour
{
    public float speed = 2f;
    public float moveDistance = 5f;

    private Vector3 initialPosition;
    private bool movingRight = true;
    private Vector3 direction;

    void Start()
    {
        initialPosition = transform.position;
        if (direction == Vector3.zero)
        {
            direction = Vector3.right;
        }
    }

    void Update()
    {
        if (IsServer || IsClient)
        {
            MoveObstacle();
        }
    }

    void MoveObstacle()
    {
        float distance = Vector3.Distance(transform.position, initialPosition);

        if (distance >= moveDistance)
        {
            movingRight = !movingRight;
            direction = -direction;
            initialPosition = transform.position;
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
