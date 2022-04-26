using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCutter : MonoBehaviour
{
    private void LateUpdate()
    {
        if (transform.localPosition == new Vector3(160, 0, -8) || transform.localPosition == new Vector3(160, 0, 328))
            Destroy(this.gameObject);
    }
}