using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salidaController : MonoBehaviour
{
    [SerializeField]
    GameManager gM;

    Quaternion cerrado;
    Quaternion abierto;

    [SerializeField]
    GameObject winScreen;

    void Start()
    {
        cerrado = Quaternion.Euler(0.0F, 88.7F, 0.0F);
        abierto = Quaternion.Euler(0.0F, -0.59F, 0.0F);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (gM.abrirPuerta)
            {
                abiertoEvento();
            }
        }
    }


    void abiertoEvento()
    {
        if (transform.rotation == cerrado)
        {
            transform.rotation = abierto;
            StartCoroutine(EsperarYCerrarPuerta());
        }
        else {
            transform.rotation = cerrado;
        }
    }

    IEnumerator EsperarYCerrarPuerta()
    {
        yield return new WaitForSeconds(1.0f);
        winScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }



}
