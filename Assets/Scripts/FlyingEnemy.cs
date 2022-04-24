using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public CharacterController characterController;
    public Transform playerCharacter;
    private float speed = 4f;
    private float rotationSpeed = 1f;

    private bool wingIsLeft = true;

    private Transform wings;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.Find("Player").transform;

        wings = transform.Find("Icosphere.001").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = playerCharacter.position - transform.position;
        if (offset.magnitude < 35f)
        {
            if (offset.magnitude < 10f)
                characterController.Move((offset + new Vector3(0, 1f, 0)).normalized * speed * Time.deltaTime);
            else
                characterController.Move((offset + new Vector3(0, 5f, 0)).normalized * speed * Time.deltaTime);

            //find the vector pointing from our position to the target
            Vector3 _direction = (playerCharacter.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            _lookRotation = Quaternion.Euler( 0, _lookRotation.eulerAngles.y, _lookRotation.eulerAngles.z);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        }

        wings.localRotation = Quaternion.Euler(-89.98f, 5f * Mathf.Sin((Time.time * 8f) * speed), 0f);
    }
}
