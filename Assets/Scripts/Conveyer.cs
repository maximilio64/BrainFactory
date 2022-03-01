using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            (collision.gameObject.GetComponent<PlayerController>() as PlayerController).conveyerDirection = PlayerController.ConveyerDirection.right;
        }
        else
            collision.gameObject.transform.position += new Vector3(.1f,0,0);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            (collision.gameObject.GetComponent<PlayerController>() as PlayerController).conveyerDirection = PlayerController.ConveyerDirection.none;
        }
    }
}
