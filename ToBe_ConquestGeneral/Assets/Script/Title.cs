using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject howToPlay;
    [SerializeField] AudioSource Submit;
    [SerializeField] AudioSource Submit2;

    public GameObject ob_window;
    bool titleNow = true;
    bool instructioonNow = false;

    void Update()
    {
        if (Input.anyKeyDown && titleNow)
        {
            Submit.Play();
            StartCoroutine(PopWindow());
        }

        if (Input.GetAxis("Jump") == 1 && titleNow == false && instructioonNow == false)
        {
            Submit.Play();
            StartCoroutine(DisappearWindow());
        }

        if (Input.GetAxis("Jump") == 1 && instructioonNow == true)
        {
            instructioonNow = false;
            Submit.Play();
            howToPlay.SetActive(false);
        }
    }

    IEnumerator PopWindow()
    {
        titleNow = false;
        ob_window.SetActive(true);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator DisappearWindow()
    {
        titleNow = true;
        ob_window.SetActive(false);
        yield return new WaitForSeconds(0.2f);
    }

    public void Elementaly()
    {
        Submit2.Play();
        StartCoroutine(GoMain());
    }

    IEnumerator GoMain()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Main");
    }

    public void Instruction()
    {
        instructioonNow = true;
        Submit2.Play();
        howToPlay.SetActive(true);
    }
}
