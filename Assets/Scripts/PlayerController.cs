using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private int count;
    public Text countText;
    public Text winText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        countText.text = "Count: " + count.ToString();
        winText.text = "";
        restartButton.gameObject.SetActive(false);
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); //disappear from scene
            count++;
            countText.text = "Count: " + count.ToString();
        }

        if (count >= 12) 
        { 
            winText.text = "You win!";
            restartButton.gameObject.SetActive(true); // show button
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); // restart the game
    }
}
