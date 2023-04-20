using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject lostScreen;

    [SerializeField]
    TextMeshProUGUI message;

    // Componente Temporizador
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    TextMeshProUGUI contadorGemas;

    int gemasContador = 0;

    //Tiempo total antes de perder
    float timer = 120.0f;

    //Flag
    bool hasLost = false;

    // Update is called once per frame
    void Update()
    {
        if (!hasLost)
        {
            timer -= Time.deltaTime;
            timerText.text = "Tiempo Restante: " + (int)timer + "s";
            if (timer <= 0)
            {
                hasLost = true;
                message.text = "Te quedaste sin tiempo";
                Debug.Log("Perdiste :(");
            }
        }
        else 
        {
            lostScreen.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void AddScoreGema(int puntoAumento) {

        if (puntoAumento >= 1) {

            gemasContador += puntoAumento;
            contadorGemas.text = "Gemas recolectadas: " + gemasContador;

        }
    
    }

}
