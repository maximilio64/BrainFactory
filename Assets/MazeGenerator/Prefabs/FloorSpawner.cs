using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public List<GameObject> toSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawned = Instantiate(toSpawn[0]);
        spawned.transform.position = transform.position + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
