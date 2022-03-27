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
    public Transform forwardCheck;
    public Transform backwardCheck;

    public Transform cameraRotator;
    public Transform cameraRotatorDummy;

    public Transform meshTransform;

    public enum ConveyerDirection { none, left, right }
    public ConveyerDirection conveyerDirection = ConveyerDirection.none;

    private Control control;

    public bool ableToMakeADoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
        meshTransform = transform.Find("Mesh");
        meshRenderer = meshTransform.GetComponent<MeshRenderer>();
        cameraRotator = transform.Find("CameraRotator").transform;
        cameraRotatorDummy = transform.Find("CameraRotatorDummy").transform;
    }

    private Transform GetCameraRotation()
    {
        cameraRotatorDummy.eulerAngles = cameraRotator.eulerAngles;
        cameraRotatorDummy.eulerAngles = new Vector3(0, cameraRotator.eulerAngles.y, 0);
        return cameraRotatorDummy;
    }

    public bool isGrounded;

    bool BothFeetOnGround()
    {
        return Physics.CheckSphere(leftFootCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(rightFootCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(forwardCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(backwardCheck.position, 0.15f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        direction.x = 0;

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

        Vector3 walkDirection = new Vector3();
        //walkDirection.x = hInput * speed;
        //walkDirection.z = vInput * speed;
        Transform currentCameraRot = GetCameraRotation();
        walkDirection += currentCameraRot.right * hInput * speed;
        walkDirection += currentCameraRot.forward * vInput * speed;
        controller.Move(walkDirection * Time.deltaTime);

        meshTransform.localRotation = Quaternion.LookRotation(walkDirection);

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

        if (transform.position.y < -2)
            TeleportToSafety();
    }

    void TeleportToSafety()
    {
        controller.enabled = false;
        control.ChangeLives(-1);
        invincibleTimer += 20;
        transform.position = lastSafePosition;
        direction = new Vector3(0, 0, 0);
        controller.enabled = true;
    }

    float invincibleTimer = 0f;

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "hurt_teleport":
                TeleportToSafety();
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
