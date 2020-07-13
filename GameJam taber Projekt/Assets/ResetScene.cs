using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public PlayerScript player;
    public PlayerScript player2;
    public GameObject resetButton;
    // Start is called before the first frame update
    void Start()
    {
        //resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Alive == false && player2.Alive == false)
        {
            resetButton.SetActive(true);
        }
    }

    public void resetSceneButton()
    {
        SceneManager.LoadScene("GameScene");
        
    }
    public void menuButton()
    {
        SceneManager.LoadScene("Menu scene");

    }
}
