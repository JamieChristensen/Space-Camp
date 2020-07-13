using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTextScroll : MonoBehaviour
{
    public AudioSource OhNoMyO2Intro;

    public float scrollSpeed;
    public RectTransform textTransform;

    public float timer;

    private void Start()
    {
        OhNoMyO2Intro.Play();

    }


    private void Update()
    {
        timer += Time.deltaTime;
        textTransform.Translate(0, scrollSpeed * Time.deltaTime, 0);
        if (timer > 58)
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        }
    }


}
