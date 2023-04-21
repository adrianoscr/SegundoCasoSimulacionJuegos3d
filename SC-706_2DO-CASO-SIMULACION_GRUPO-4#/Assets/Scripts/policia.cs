using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policia : MonoBehaviour
{
    public float Rango;

    public LayerMask personaje;
    public Transform jugador;
    public float velocidad;


    bool Alerta;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        Alerta = Physics.CheckSphere(transform.position, Rango, personaje);

        if(Alerta == true)
        {
            transform.LookAt(new Vector3(jugador.position.x,transform.position.y,jugador.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x, transform.position.y, jugador.position.z), velocidad * Time.deltaTime);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Rango);



    }

}
