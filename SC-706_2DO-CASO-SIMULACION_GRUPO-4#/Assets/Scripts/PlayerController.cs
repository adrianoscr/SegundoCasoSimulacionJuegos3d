using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField]
    float walkFactor = 5.0F;

    [SerializeField]
    float runFactor = 5.0F;

    [SerializeField]
    float rotationFactor = 100.0F;


    [Header("Jump")]
    [SerializeField]
    float gravityMultiplier = 3.0F;

    [SerializeField]
    float jumpForce = 8.0F;

    [SerializeField]
    int maximunNumberOfJumps = 2;

    CharacterController character;

    Vector3 direccion;

    float inputX;
    float inputZ;
    float magnitude;
    float gravityY;
    float velocityY;

    bool isRunnig;
    bool isMovePressed;
    bool isJumpPressed;

    int numberOfJumps;

    void Awake()
    {
        character = GetComponent<CharacterController>();

        gravityY = Physics.gravity.y;

    }

    void Update()
    {
        handleInputs();
        HandleGravity();
        handleMove();
        handleRotation();
    }


    void HandleGravity()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
            if (velocityY < -1.0F)
            {
                velocityY = -1.0F;
            }

            if (isJumpPressed)
            {
                handleJump();
                StartCoroutine(WaitForGroundCorutine());
            }
        }
        else
        {

            if (isJumpPressed && maximunNumberOfJumps > 1)
            {
                handleJump();
            }
            velocityY += gravityY * gravityMultiplier * Time.deltaTime;
        }


    }

    void handleInputs()
    {

        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");


        //DETECTAR CUANDO SE ESTA MOVIENDO
        isMovePressed = inputX != 0.0F || inputZ != 0.0F;
        isRunnig = Input.GetButton("Fire3");
        isJumpPressed = Input.GetButtonDown("Jump");
    }

    void handleJump()
    {
        if (numberOfJumps > maximunNumberOfJumps)
        {
            return;
        }

        numberOfJumps++;
        velocityY = jumpForce / numberOfJumps;

    }

    void handleMove()
    {
        direccion = new Vector3(inputX, 0.0F, inputZ);
        direccion = Camera.main.transform.forward * direccion.z + Camera.main.transform.right * direccion.x;
        direccion.y = 0.0F;

        //AVERIGUAR DIRECCION DEL MOVIMIENTO
        magnitude = Mathf.Clamp01(direccion.magnitude);
        direccion.Normalize();

        //Correr o caminar
        Vector3 velocity = direccion * magnitude *
            (isRunnig
                ? runFactor
                : walkFactor);

        //CON ESTO LE DAMOS INPULSO HACIA EL PISO
        velocity.y = velocityY;

        //MOVIMIENTO UNIFORME DURANTE EL TIEMPO
        character.Move(velocity * Time.deltaTime);
    }


    void handleRotation()
    {
        if (isMovePressed)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            if (isMovePressed)
            {
                if (horizontalInput != 0)
                {
                    transform.Rotate(Vector3.up, horizontalInput * rotationFactor * Time.deltaTime);
                }
            }
        }
    }

    bool IsGrounded()
    {
        return character.isGrounded;
    }


    IEnumerator WaitForGroundCorutine()
    {

        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        numberOfJumps = 0;

    }
}
