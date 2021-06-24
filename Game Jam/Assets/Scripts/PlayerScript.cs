using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int playerHealth = 3;

    public float chargeTime = 2f;
    

    public float invincibleLength = 1;
    public float invincibleFlickerRate = 0.3f;

    private float flickerTimer = 0;
    private float invincibleTimer = 0;
    private bool currentlyInvincible = false;
    private Vector3 userInput = Vector3.zero;
    private Rigidbody playerRb = null;
    private MeshRenderer playerRenderer = null;
    private GameStateManager gameManager = null;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
        gameManager = FindObjectOfType<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            //Game over
            transform.position = new Vector3(0, 6, 0);
            gameManager.PlayerDead();
        }
        if (currentlyInvincible)
        {
            invincibleTimer += Time.deltaTime;
            flickerTimer += Time.deltaTime;
            if (flickerTimer > invincibleFlickerRate)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flickerTimer = 0;
            }
            if (invincibleTimer >= invincibleLength)
            {
                currentlyInvincible = false;
                invincibleTimer = 0;
                playerRenderer.enabled = true;
                flickerTimer = 0;
            }
        }
        userInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        playerRb.velocity = userInput * moveSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (!currentlyInvincible)
            {
                playerHealth--;

                if (playerHealth > 0)
                    currentlyInvincible = true;
            }
        }
        
    }
}
