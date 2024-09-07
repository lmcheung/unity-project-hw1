using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private float timer;
    private int seconds;
    private int surviveTimer;
    public Text timerText;
    public Text winText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = 0.0f;
        surviveTimer = 60;
        timerText.text = "Timer: " + surviveTimer.ToString();
        winText.text = "";
        restartButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (seconds != 60) // Keeps counting until timer has hit 60 seconds. 
        {
            timer += Time.deltaTime;
            seconds = (int)timer;
        } else if (seconds == 60)
        {
            winText.text = "You win!"; // displays win text
            restartButton.gameObject.SetActive(true); // show restart button
        }
        
        // Displays the countdown timer 
        timerText.text = "Timer: " + (surviveTimer - seconds).ToString();
        
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); // restart the game
    }
}
