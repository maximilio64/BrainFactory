using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;

    private Transform pos1;
    private Transform pos2;

    private Transform currentGoal;
    private bool goalIsPos1;

    private Transform platform;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.parent.Find("Pos1");
        pos2 = transform.parent.Find("Pos2");

        currentGoal = pos2;
        goalIsPos1 = false;

        platform = transform.parent.Find("Platform");
    }

    Vector3 change;

    // Update is called once per frame
    void Update()
    {
        Vector3 oldPosition = platform.position;
        platform.position = Vector3.MoveTowards(platform.position, currentGoal.position, speed * Time.deltaTime);
        Vector3 newPosition = platform.position;
        change = newPosition - oldPosition;
        if (platform.position == currentGoal.position)
        {
            if (goalIsPos1)
            {
                goalIsPos1 = false;
                currentGoal = pos2;
            } else
            {
                goalIsPos1 = true;
                currentGoal = pos1;
            }
        }
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        (collision.gameObject.GetComponent<CharacterController>() as CharacterController).enabled = false;
    //        collision.gameObject.transform.position += change;
    //        (collision.gameObject.GetComponent<CharacterController>() as CharacterController).enabled = true;
    //    }
    //}
}
