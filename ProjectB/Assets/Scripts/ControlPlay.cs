using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPlay : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioRocket;
    Quaternion rotacionInicial;
    ParticleSystem explosionParticles;
    AudioSource sonidoExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioRocket = GetComponent<AudioSource>();
        rotacionInicial = transform.rotation;
        explosionParticles = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
        sonidoExplosion = GameObject.Find("Explosion").GetComponent<AudioSource>();
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
            rb.AddRelativeForce(Vector3.up * 0.2f, ForceMode.VelocityChange);

            var rotarIzq = transform.rotation;
            rotarIzq.x += Time.deltaTime * 0.2f;
            transform.rotation = rotarIzq;

            if (!audioRocket.isPlaying)
            {
                audioRocket.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            audioRocket.Stop();
            StartCoroutine(RotarSuavemente(rotacionInicial));
        }
    }
    private void bajar()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.down * 0.2f, ForceMode.VelocityChange);

            var rotarDer = transform.rotation;
            rotarDer.x -= Time.deltaTime * 0.2f;
            transform.rotation = rotarDer;

            if (!audioRocket.isPlaying)
            {
                audioRocket.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            audioRocket.Stop();
            StartCoroutine(RotarSuavemente(rotacionInicial));
        }
    }

    private IEnumerator RotarSuavemente(Quaternion rotacionObjetivo)
    {
        float duracionRotacion = 0.5f;
        float tiempoPasado = 0f;

        Quaternion rotacionInicial = transform.rotation;

        while (tiempoPasado < duracionRotacion)
        {
            tiempoPasado += Time.deltaTime;
            float porcentajeCompletado = tiempoPasado / duracionRotacion;

            transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, porcentajeCompletado);

            yield return null;
        }
        transform.rotation = rotacionObjetivo;
    }

    private void adelante()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * 0.05f, ForceMode.VelocityChange);
            if (!audioRocket.isPlaying)
            {
                audioRocket.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            audioRocket.Stop();
        }
    }
    private void atras()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * 0.05f, ForceMode.VelocityChange);
            if (!audioRocket.isPlaying)
            {
                audioRocket.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            audioRocket.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ColisionSegura":
                print ("OK");
                StartCoroutine(CambiarEscena());
                break;

            case "ColisionPeligrosa":
                print("HIT");
                sonidoExplosion.Play();
                StartCoroutine(ProcesoColisionPeligrosa());
                break;
        }
    }

    private IEnumerator ProcesoColisionPeligrosa() 
    {
        Vector3 posicionNave = transform.position;
        if (explosionParticles != null)
        {
            explosionParticles.transform.position = posicionNave;
            explosionParticles.Play();
        }
        gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        print("Game Over");

        Application.Quit();
    }
    private IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Nivel2");
    }
}
