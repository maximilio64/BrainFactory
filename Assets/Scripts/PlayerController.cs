using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public MeshRenderer meshRenderer;
    public Vector3 direction;
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 lastSafePosition;

    public Transform leftFootCheck;
    public Transform rightFootCheck;

    public enum ConveyerDirection { none, left, right }
    public ConveyerDirection conveyerDirection = ConveyerDirection.none;

    private Control control;

    public bool ableToMakeADoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("Control").GetComponent<Control>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public bool isGrounded;

    bool BothFeetOnGround()
    {
        return Physics.CheckSphere(leftFootCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(rightFootCheck.position, 0.15f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        switch (conveyerDirection)
        {
            case ConveyerDirection.left:
                direction.x -= 5;
                break;
            case ConveyerDirection.right:
                direction.x += 5;
                break;
        }

        //isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        if (BothFeetOnGround())
        {
            lastSafePosition = transform.position;
        }

        if (isGrounded)
        {
            direction.y = -1;
            ableToMakeADoubleJump = true;
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

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        invincibleTimer = Mathf.MoveTowards(invincibleTimer, 0f, Time.deltaTime * 40f);
        if (invincibleTimer > 0)
        {
            if (invincibleTimer % 10 > 5 && invincibleTimer > 10)
                meshRenderer.enabled = false;
            else
                meshRenderer.enabled = true;
        } else
            meshRenderer.enabled = true;
    }

    float invincibleTimer = 0f;

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "hurt_teleport":
                controller.enabled = false;
                control.ChangeLives(-1);
                invincibleTimer += 20;
                transform.position = lastSafePosition;
                controller.enabled = true;
                break;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "hurt":
                if (invincibleTimer == 0)
                {
                    control.ChangeLives(-1);
                    invincibleTimer = 100f;
                }
                break;
        }
    }
}
