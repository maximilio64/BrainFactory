using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Transform door;
    private void Start()
    {
        door = transform.Find("Door").transform;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(Open());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(Close());
        }
    }

    IEnumerator Open()
    {
        while (true)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, new Vector3(-63.5999985f, 0, 30.2000008f), 100f * Time.deltaTime);
            //door.localEulerAngles = Vector3.RotateTowards(door.localEulerAngles, new Vector3(90, 0, 0), 1f * Time.deltaTime, 1f * Time.deltaTime);
            door.localRotation = Quaternion.RotateTowards(door.localRotation, Quaternion.Euler(90, 233.10968f, 0), 200f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Close()
    {
        while (true)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, new Vector3(0, 0, 0), 100f * Time.deltaTime);
            //door.localEulerAngles = Vector3.RotateTowards(door.localEulerAngles, new Vector3(90, 0, 0), 1f * Time.deltaTime, 1f * Time.deltaTime);
            door.localRotation = Quaternion.RotateTowards(door.localRotation, Quaternion.Euler(89.98f, 0, 0), 200f * Time.deltaTime);
            yield return null;
        }
    }
}
