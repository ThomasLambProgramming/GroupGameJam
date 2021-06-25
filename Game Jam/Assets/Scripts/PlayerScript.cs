using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int playerHealth = 3;

    public float chargeNeeded = 16f;
    public float chargePerSecond = 2f;
    private float chargeAmount = 0;
    private bool isCharged = false;

    public ParticleSystem playerHitParticle = null;

    public float invincibleLength = 1;
    public float invincibleFlickerRate = 0.3f;
    
    private float flickerTimer = 0;
    private float invincibleTimer = 0;
    private bool currentlyInvincible = false;
    private Vector3 userInput = Vector3.zero;
    private Rigidbody playerRb = null;
    private MeshRenderer playerRenderer = null;
    private GamestateManager gameManager = null;
    private AudioSource generatorSound = null;
    void Start()
    {
        generatorSound = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
        gameManager = FindObjectOfType<GamestateManager>();
    }
    public bool PlayerCharged() => chargeAmount > chargeNeeded ? true : false;
    public void ChargeUsed()
    {
        isCharged = false;
        chargeAmount = 0;
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
        if (userInput.sqrMagnitude > 0.5f && !isCharged)
        {
            if (chargeAmount < chargeNeeded)
                chargeAmount += chargePerSecond * Time.deltaTime;
            else
            {
                IsCharged();
            }
        }
    }
    private void IsCharged()
    {
        generatorSound.Play();
        isCharged = true;
        //play sound
        //change light thing
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
                //playerHitParticle.Play();
                if (playerHealth > 0)
                    currentlyInvincible = true;
            }
        }
        
    }
}
