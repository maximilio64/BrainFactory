using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(0, 50* Mathf.Sin((Time.time * 8f) * speed) + 50);
        skinnedMeshRenderer.SetBlendShapeWeight(1, 50* Mathf.Sin((Time.time * 6f) * speed) + 50);
    }
}
