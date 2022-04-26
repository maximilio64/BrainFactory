using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float scale = 0;

    public int id;
    public void SetID(int i)
    {
        id = i;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        scale += Time.deltaTime * 5f;
        transform.localScale = new Vector3(scale, scale, scale);
        if (scale >= 10f)
            Destroy(this.gameObject);
        
    }
}
