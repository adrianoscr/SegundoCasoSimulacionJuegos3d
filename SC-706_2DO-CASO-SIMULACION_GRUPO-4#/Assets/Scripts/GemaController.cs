using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GemaController : MonoBehaviour
{
    [SerializeField]
    int takePoints = 1;

    bool isTaken;

    [SerializeField]
    GameManager gM;

    float rotationSpeed = 100.0F;

    void OnTriggerEnter(Collider other)
    {



        if (other.CompareTag("Player"))
        {

            gM.AddScoreGema(takePoints);
            Destroy(gameObject);
            

        }


    }


    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

}
