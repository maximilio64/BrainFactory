using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(27f);
        SaveData.playerBrainStartLoc = new Vector3(-87.8170776f, 178.330002f, -114.744286f);
        SceneManager.LoadScene("Brain");
    }
}
