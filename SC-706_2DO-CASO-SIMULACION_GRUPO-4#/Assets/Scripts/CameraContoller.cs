using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 3.0f;

    private float vRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float h = sensitivity * Input.GetAxis("Mouse X");
        float v = sensitivity * Input.GetAxis("Mouse Y");

        vRotation -= v;

        vRotation = Mathf.Clamp(vRotation, -60, 60);

        transform.localRotation = Quaternion.Euler(vRotation, 0, 0);
        player.transform.Rotate(0, h, 0);
    }
}
