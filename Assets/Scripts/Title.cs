using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject instructions;

    private void Start()
    {
        StartCoroutine(ShowInstructions());
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SaveData.canSkipTitle)
        {
            Application.Quit();
        }

        if (Input.anyKey && SaveData.canSkipTitle)
        {
            SceneManager.LoadScene("Brain");
        }
    }

    IEnumerator ShowInstructions()
    {
        yield return new WaitForSeconds(4f);
        instructions.SetActive(true);
        SaveData.canSkipTitle = true;
    }
}
