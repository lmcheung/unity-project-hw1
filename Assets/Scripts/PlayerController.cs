// Author: Lok Cheung
// Date: September 22, 2024
// Handles the Player logic, powerup effects and also UI text.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject powerUp;
    public float speed;
    private float timer;
    private int seconds;
    private int surviveTimer;
    private bool pauseTimer;
    private bool powerUpActive;
    private int count;
    public Text timerText;
    public Text winText;
    public Text loseText;
    public Text countText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Call ShowPowerUp() every 3 seconds without any delay.
        InvokeRepeating("ShowPowerUp", 0f, 3f);

        // Timers
        timer = 0.0f;
        surviveTimer = 60; // sets the default timer to count down from

        // Flags
        pauseTimer = false; // Boolean flag to handle pausing the timer
        powerUpActive = false; // Boolean flag to handle activating and deactivating the powerup

        // Initial Text
        timerText.text = "Timer: " + surviveTimer.ToString(); // Displays the timer
        countText.text = "Powerup: " + count.ToString(); // Displays the powerup collection
        winText.text = "";
        loseText.text = "";

        // Msc. stuff
        speed = 5f; // Setting initial speed to 
        count = 0;
        restartButton.gameObject.SetActive(false); // Hide the restart button 
    }

    void Update()
    {
        if ((seconds != 60) && (pauseTimer != true)) // Keeps counting until timer has hit 60 seconds. 
        {
            timer += Time.deltaTime;
            seconds = (int)timer;
        } else if (seconds == 60) // 60 seconds has elapse. Player win!
        {
            winText.text = "You win!"; // displays win text
            restartButton.gameObject.SetActive(true); // show restart button
            pauseTimer = true; // Pauses the timer

            // Run if the powerup was never activated.
            if (powerUpActive == false)
            {
                CleanPowerUpLogic();
            }
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

    // Handles PowerUp Object randomizer 
    void ShowPowerUp()
    {
        // Only triggers when the flag variable is false
        if (!powerUpActive)
        {
            powerUp.SetActive(true);

            // Setting up the boundires so it doesn't go outside of the map
            float randXCord = Random.Range(-12f, 12f);
            float randYCord = Random.Range(-12f, 12f);

            // PowerUp appears randomly using randXCord and randYCord
            powerUp.transform.position = new Vector2(randYCord, randXCord);
        }
    }

    // Handles the clean up logic for Power up when game is over.
    void CleanPowerUpLogic()
    {
        powerUpActive = true; // Disables the powerup generation
        powerUp.SetActive(false); // Hides the Powerup spirite
    }

    // Handles the game over if Player touches a PickUp
    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("PickUp")) && (pauseTimer != true))
        {
            loseText.text = "Game Over!"; // Show Game Over Text
            restartButton.gameObject.SetActive(true); // Show the Restart Button
            pauseTimer = true; // Pauses the timer
            CleanPowerUpLogic();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // When collecting a powerup 
        if (col.gameObject.CompareTag("PowerUp")) {
            powerUp.SetActive(false);
            count++;
            countText.text = "Powerup: " + count.ToString(); // Update text
        }
        
        // Needs to reach 5 power up collection before effect activates
        if (count >= 5)
        {
            // Activate the below effects
            countText.text = "Powerup: Activated!";
            speed = 10f;
            powerUpActive = true;
        }
    }

    void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); // restart the game
    }
}
