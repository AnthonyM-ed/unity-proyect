using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(3f, 12f, -32f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private float minXLimit = 3f; // Valor m�nimo permitido en el eje X
    [SerializeField] private float maxXLimit = 100f;  // Valor m�ximo permitido en el eje X

    private void Update()
    {
        // Calcula la posici�n objetivo de la c�mara
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, minXLimit, maxXLimit),
            transform.position.y,
            offset.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
