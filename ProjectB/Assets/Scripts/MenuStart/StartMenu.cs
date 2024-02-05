using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void jugar(){
        SceneManager.LoadScene("Nivel1");
    }
    public void Salir() {

        //Debug.Log("Salir...");
        Application.Quit();  //Cerrar la aplicación
        //Application.Close();
    } 
}