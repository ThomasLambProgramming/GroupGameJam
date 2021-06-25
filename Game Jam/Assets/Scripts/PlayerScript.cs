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
    public AudioClip hitSound = null;

    public float invincibleLength = 1;
    public float invincibleFlickerRate = 0.3f;
    public float shieldSpinSpeed = 10f;
    private float flickerTimer = 0;
    private float invincibleTimer = 0;
    private bool currentlyInvincible = false;
    private Vector3 userInput = Vector3.zero;
    private Rigidbody playerRb = null;
    private MeshRenderer playerRenderer = null;
    private GamestateManager gameManager = null;
    private AudioSource generatorSound = null;

    public GameObject BlueShield = null;
    public GameObject OrangeShield = null;
    public GameObject RedShield = null;
    private GameObject currentShield = null;
    private MeshRenderer currentShieldRenderer = null;
    private SphereCollider currentShieldCollider = null;

    public GameObject invincibleStopper = null;
    private SphereCollider invincibleCollider = null;

    void Start()
    {
        invincibleCollider = invincibleStopper.GetComponent<SphereCollider>();
        currentShield = BlueShield;
        generatorSound = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
        gameManager = FindObjectOfType<GamestateManager>();
        currentShieldRenderer = currentShield.GetComponent<MeshRenderer>();
        currentShieldCollider = currentShield.GetComponent<SphereCollider>();
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
        float spinAmount = shieldSpinSpeed * Time.deltaTime;
        currentShield.transform.Rotate(new Vector3(spinAmount, spinAmount, spinAmount));
        if (playerHealth <= 0)
        {
            //Game over
            gameManager.PlayerDead();
        }
        if (currentlyInvincible)
        {
            invincibleTimer += Time.deltaTime;
            flickerTimer += Time.deltaTime;
            if (flickerTimer > invincibleFlickerRate)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                currentShieldRenderer.enabled = !currentShieldRenderer.enabled;
                flickerTimer = 0;
            }
            if (invincibleTimer >= invincibleLength)
            {
                invincibleCollider.enabled = false;
                currentShieldCollider.enabled = true;
                currentlyInvincible = false;
                invincibleTimer = 0;
                playerRenderer.enabled = true;
                currentShieldRenderer.enabled = true;
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
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
                invincibleCollider.enabled = true;
                playerHealth--;
                if (playerHealth == 2)
                {
                    //orange
                    OrangeShield.SetActive(true);
                    currentShield = OrangeShield;
                    BlueShield.SetActive(false);
                    currentShieldRenderer = currentShield.GetComponent<MeshRenderer>();
                    currentShieldCollider = currentShield.GetComponent<SphereCollider>();
                }
                if (playerHealth == 1)
                {
                    RedShield.SetActive(true);
                    currentShield = RedShield;
                    OrangeShield.SetActive(false);
                    currentShieldRenderer = currentShield.GetComponent<MeshRenderer>();
                    currentShieldCollider = currentShield.GetComponent<SphereCollider>();
                }
                //disable so its not doing collisions when invincible because of how big it is
                currentShieldCollider.enabled = false;
                //playerHitParticle.Play();
                if (playerHealth > 0)
                    currentlyInvincible = true;
            }
        }
        
    }
}
