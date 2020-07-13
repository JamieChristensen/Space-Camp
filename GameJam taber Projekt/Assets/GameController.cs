using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static int kills;
    public float timeLeft=120;
    public PlayerScript play1;
    public PlayerScript play2;
    public Text timeText;
    public Text killText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killText.text = "" + kills;
        timeText.text = "" + Mathf.Floor(timeLeft);
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        play1.TakeDamage(1000);
        play2.TakeDamage(1000);
    }
}
