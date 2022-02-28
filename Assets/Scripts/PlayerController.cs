using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 lastSafePosition;

    private Control control;

    public bool ableToMakeADoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("Control").GetComponent<Control>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        if (isGrounded)
        {
            direction.y = 0;
            ableToMakeADoubleJump = true;
            lastSafePosition = transform.position;
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        } else
        {
            direction.y += gravity * Time.deltaTime;
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                ableToMakeADoubleJump = false;
            }
        }
        controller.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "hurt_teleport":
                controller.enabled = false;
                control.ChangeLives(-1);
                transform.position = lastSafePosition;
                controller.enabled = true;
                Debug.Log("dfsef");
                break;
        }
    }
}
