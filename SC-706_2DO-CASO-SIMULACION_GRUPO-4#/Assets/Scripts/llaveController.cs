using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llaveController : MonoBehaviour
{
    [SerializeField]
    int takePoints = 1;

    bool isTaken;

    [SerializeField]
    GameManager gM;

    float verticalSpeed = 2.0F;

    Vector3 posicionInicio;

     void Start()
    {
        posicionInicio = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {



        if (other.CompareTag("Player"))
        {

            gM.ActivacionSalida();
            Destroy(gameObject);


        }


    }


    void Update()
    {
        float verticalOffset = Mathf.Sin(Time.time * verticalSpeed) * 0.5f;
        transform.position = posicionInicio + new Vector3(0.0f, verticalOffset, 0.0f);
    }
}
