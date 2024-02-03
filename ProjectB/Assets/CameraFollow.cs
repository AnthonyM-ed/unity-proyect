using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(3f, 12f, -32f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private float minXLimit = 3f; // Valor mínimo permitido en el eje X
    [SerializeField] private float maxXLimit = 100f;  // Valor máximo permitido en el eje X

    private void Update()
    {
        // Calcula la posición objetivo de la cámara
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, minXLimit, maxXLimit),
            transform.position.y,
            offset.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
