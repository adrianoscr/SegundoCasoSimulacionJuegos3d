using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    [SerializeField]
    float force = 2.0F;

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        // Si no es Pushable entonces sale del metodo
        if (!other.collider.CompareTag("Pushable"))
        {
            return;
        }

        Rigidbody rb = other.collider.GetComponent<Rigidbody>();
        // Si tiene Rigidbody entonces sale del metodo
        if (rb == null)
        {
            return;
        }

        // Obtiene la dirreción donde debe ser empujado el objeto
        Vector3 direction =
            other.gameObject.transform.position - transform.position;
        direction.y = 0.0F;
        direction.Normalize();

        // Empuja el objeto hacia la dirreción obtenida
        rb.AddForceAtPosition
            (direction * force, transform.position, ForceMode.Impulse);
    }
}
