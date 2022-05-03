using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 direction;
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask unsafeGroundLayer;
    public LayerMask mushroomLayer;
    public Vector3 lastSafePosition;

    public AudioClip after;

    public Transform leftFootCheck;
    public Transform rightFootCheck;
    public Transform forwardCheck;
    public Transform backwardCheck;

    Quaternion nonzeroWalkRotation;

    public Transform cameraRotator;
    public Transform cameraRotatorDummy;

    public Vector3 enemyLaunch = new Vector3();

    public Transform meshTransform;

    public Vector3 conveyerDirection = new Vector3(0,0,0);

    private Control control;

    public Animator animator;

    public GameObject explosion;
    public GameObject platform;
    public GameObject currentPlatformRef;

    public bool ableToMakeADoubleJump = true;

    float powerupCoolDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Control>();
        cameraRotator = transform.Find("CameraRotator").transform;
        cameraRotatorDummy = transform.Find("CameraRotatorDummy").transform;
    }

    private Transform GetCameraRotation()
    {
        cameraRotatorDummy.eulerAngles = cameraRotator.eulerAngles;
        cameraRotatorDummy.eulerAngles = new Vector3(0, cameraRotator.eulerAngles.y, 0);
        return cameraRotatorDummy;
    }

    public void changeMusic()
    {
        GetComponent<AudioSource>().clip = after;
        GetComponent<AudioSource>().Play();
    }

    public bool isGrounded;

    bool BothFeetOnGround()
    {
        return Physics.CheckSphere(leftFootCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(rightFootCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(forwardCheck.position, 0.15f, groundLayer) && Physics.CheckSphere(backwardCheck.position, 0.15f, groundLayer);
    }

    bool OnMushroom()
    {
        return Physics.CheckSphere(leftFootCheck.position, 0.15f, mushroomLayer) || Physics.CheckSphere(rightFootCheck.position, 0.15f, mushroomLayer) || Physics.CheckSphere(forwardCheck.position, 0.15f, mushroomLayer) || Physics.CheckSphere(backwardCheck.position, 0.15f, mushroomLayer);
    }

    public void playCoinSound()
    {
        transform.Find("ground check").GetComponent<AudioSource>().Play();
    }

    int explodeNum = 0;

    // Update is called once per frame
    void Update()
    {
        bool isImmobile = animator.GetCurrentAnimatorStateInfo(0).IsName("Sew");

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        powerupCoolDown -= Time.deltaTime;
        if (powerupCoolDown < 0)
            powerupCoolDown = 0;

        if (powerupCoolDown <= 0 && SaveData.hasAttackPower && Input.GetKeyDown("e"))
        {
            powerupCoolDown = 5;
            GameObject e = Instantiate(explosion);
            e.GetComponent<Explosion>().SetID(explodeNum);
            Debug.Log(explodeNum);
            explodeNum++;
            e.transform.SetParent(this.transform);
            e.transform.localPosition = new Vector3(0, 1f, 0);
        }
        if (SaveData.hasPlatformPower && Input.GetKeyDown("q"))
        {
            powerupCoolDown = 5;
            GameObject e = Instantiate(platform);
            e.transform.position = transform.position + new Vector3(0, 0, 0);
            e.transform.rotation = meshTransform.localRotation;
            e.transform.position += e.transform.forward * 10;

            //animator.SetTrigger("sew");
        }

        direction.x = 0;
        direction.z = 0;

        direction += conveyerDirection;

        //isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        if (OnMushroom())
        {
            direction.y = 30;
            animator.SetTrigger("Jump");
            ableToMakeADoubleJump = true;
            meshTransform.GetComponent<AudioSource>().Play();
        }

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
                animator.SetTrigger("Jump");

            }
        } else
        {
            direction.y += gravity * Time.deltaTime;
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                animator.SetTrigger("Jump");
                ableToMakeADoubleJump = false;
            }
        }
        controller.Move(direction * Time.deltaTime);

        float runMultiplier = (Input.GetKey("left shift")) ? 2f : 1f;

        Vector3 walkDirection = new Vector3();
        //walkDirection.x = hInput * speed;
        //walkDirection.z = vInput * speed;
        Transform currentCameraRot = GetCameraRotation();
        walkDirection += currentCameraRot.right * hInput * speed * runMultiplier;
        walkDirection += currentCameraRot.forward * vInput * speed * runMultiplier;
        controller.Move(walkDirection * Time.deltaTime);

        controller.Move(enemyLaunch * Time.deltaTime);
        enemyLaunch = Vector3.MoveTowards(enemyLaunch, new Vector3(0, 0, 0), Time.deltaTime * 10f);

        animator.SetFloat("speed", walkDirection.magnitude / 10f);

        if (walkDirection.magnitude != 0)
            nonzeroWalkRotation = meshTransform.localRotation;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        meshTransform.localRotation = Quaternion.LookRotation(walkDirection);

        if (walkDirection.magnitude == 0)
            meshTransform.localRotation = nonzeroWalkRotation;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer) || Physics.CheckSphere(groundCheck.position, 0.15f, unsafeGroundLayer);

        invincibleTimer = Mathf.MoveTowards(invincibleTimer, 0f, Time.deltaTime * 40f);
        if (invincibleTimer > 0)
        {
            if (invincibleTimer % 10 > 5 && invincibleTimer > 10)
                meshTransform.gameObject.SetActive(false);
            else
                meshTransform.gameObject.SetActive(true);
        } else
            meshTransform.gameObject.SetActive(true);

        if (transform.position.y < -2)
            TeleportToSafety();
    }

    void TeleportToSafety()
    {
        controller.enabled = false;
        control.ChangeLives(-1);
        invincibleTimer = 100;
        transform.position = lastSafePosition;
        direction = new Vector3(0, 0, 0);
        controller.enabled = true;
    }

    public float invincibleTimer = 0f;

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
