using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    GamestateManager gameManager = null;
    public float cooldown = 1f;
    private float cooldownTimer = 0;
    private bool enableCoolDown = false;
    public ParticleSystem playerPressed = null;
    private Generator generator1 = null;
    private Generator generator2 = null;
    public AudioClip flickSound = null;
    // Start is called before the first frame update
    void Start()
    {
        Generator[] foundGenerators = FindObjectsOfType<Generator>();
        generator1 = foundGenerators[0];
        generator2 = foundGenerators[1];
        gameManager = FindObjectOfType<GamestateManager>();
    }
    private void Update()
    {
        if (enableCoolDown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > cooldown)
            {
                enableCoolDown = false;
                cooldownTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!enableCoolDown)
            {
                generator1.SwitchUsed();
                generator2.SwitchUsed();
                //playerPressed.Play();
                gameManager.FlipWallAxis();
                enableCoolDown = true;
                AudioSource.PlayClipAtPoint(flickSound, Vector3.zero);
            }
        }
    }
}
