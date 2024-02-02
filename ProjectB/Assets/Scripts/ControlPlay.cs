using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlay : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    Quaternion rotacionInicial;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rotacionInicial = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        getInputKeyboard();
    }

    void getInputKeyboard ()
    {
        subir();
        bajar();
        adelante();
        atras();
    }
    private void subir()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * 0.5f, ForceMode.VelocityChange);

            var rotarIzq = transform.rotation;
            rotarIzq.x += Time.deltaTime * 0.2f;
            transform.rotation = rotarIzq;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            audioSource.Stop();
            StartCoroutine(RotarSuavemente(rotacionInicial));
        }
    }
    private void bajar()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.down * 0.5f, ForceMode.VelocityChange);

            var rotarDer = transform.rotation;
            rotarDer.x -= Time.deltaTime * 0.2f;
            transform.rotation = rotarDer;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            audioSource.Stop();
            StartCoroutine(RotarSuavemente(rotacionInicial));
        }
    }

    // Función para rotar suavemente hacia una rotación objetivo
    private IEnumerator RotarSuavemente(Quaternion rotacionObjetivo)
    {
        float duracionRotacion = 0.5f; // Puedes ajustar la duración según sea necesario
        float tiempoPasado = 0f;

        Quaternion rotacionInicial = transform.rotation;

        while (tiempoPasado < duracionRotacion)
        {
            tiempoPasado += Time.deltaTime;
            float porcentajeCompletado = tiempoPasado / duracionRotacion;

            // Aplicar la rotación suavemente usando Slerp
            transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, porcentajeCompletado);

            yield return null;
        }

        // Asegurarse de que la rotación final sea exactamente la rotación objetivo
        transform.rotation = rotacionObjetivo;
    }

    private void adelante()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * 0.2f, ForceMode.Impulse);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            audioSource.Stop();
        }
    }
    private void atras()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * 0.2f, ForceMode.Impulse);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ColisionSegura":
                print ("OK");
                break;

            case "ColisionPeligrosa":
                print("HIT");
                break;
        }
    }
}
