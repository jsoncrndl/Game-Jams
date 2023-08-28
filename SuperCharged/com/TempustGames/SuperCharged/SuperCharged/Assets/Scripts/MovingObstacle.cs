using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance;

    private Vector3 startPos;
    [SerializeField] private Vector2 moveDirection = Vector2.up;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateSpeed != 0)
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (moveSpeed != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos + (Vector3)moveDirection * moveDistance, moveSpeed * Time.deltaTime);
            if (transform.position == startPos + (Vector3)moveDirection * moveDistance)
            {
                moveDirection = -moveDirection;
            }
        }
    }
}
