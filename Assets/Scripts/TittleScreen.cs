using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
