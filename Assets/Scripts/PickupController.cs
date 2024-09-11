// Author: Lok Cheung
// Date: September 22, 2024
// Handles the random movement of the PickUp logic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 randomMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // Random movements of Pickup objects
        randomMovement = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        // Control speed of PickUp objects.
        rb2d.velocity = randomMovement * 2.0f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Change direction when a collision occurs
        randomMovement = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
    }
}
