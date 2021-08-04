using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))] 




public class FPSKarakterKontrol : MonoBehaviour
{
    [Header("Jump")]
    public Transform groundCheck; // karakterin altında gameobject gerekiyor. 
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f; 
    Vector3 velocity;
    float gravity = -9.81f;
    bool isGrounded;


    [Header("Stamina")]
    public Image StaminaImage; // filled tipinde image gerekiyor.
    public float FastSpeed = 10;
    public float LowSpeed = 4;

    public float StaminaFillSpeed = 0.04f;
    public float StaminaDropSpeed = 0.02f;

    [Range(0, 1)]
    float staminaValue = 1;


    [Header("Movement")]
    public Transform camera; // Cinemachine gerekiyor. 
    public LayerMask groundMask;
    public float turnSmoothTime = 0.1f;
    public float speed = 6f;
    CharacterController controller;
    float turnSmoothVelocity;
    bool isFastRun;



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        StaminaImage.fillAmount = staminaValue;
        Movemement();
        Jump();
        FastRun();
    }

    
    void Jump()
    {
        #region Jump


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        #endregion
    }

    void Movemement()
    {
        #region Move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion
    }

    void FastRun()
    {
        #region FastRun
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && staminaValue >= 0.8f)
        {
            CancelInvoke("Staminadoldur");
            speed = FastSpeed;
            InvokeRepeating("StaminaAzalt", 0f, 0.2f);


        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            CancelInvoke("StaminaAzalt");
            speed = LowSpeed;
            InvokeRepeating("Staminadoldur", 0f, 0.2f);

        }
        #endregion
    }

    #region staminafunction
    void StaminaAzalt()
    {
        if (staminaValue >= 0)
            staminaValue -= 0.02f;
        if (staminaValue<=0)
        {
            speed = LowSpeed;
        }
    }

    void Staminadoldur()
    {
        if (staminaValue <= 1)
        {
            staminaValue += 0.04f;
        }
    }
    #endregion

}