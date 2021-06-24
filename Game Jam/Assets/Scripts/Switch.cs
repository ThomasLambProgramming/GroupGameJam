using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    GameStateManager gameManager = null;
    public float cooldown = 1f;
    private float cooldownTimer = 0;
    private bool enableCoolDown = false;
    public ParticleSystem playerPressed = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameStateManager>();
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
                //playerPressed.Play();
                gameManager.FlipWallAxis();
                enableCoolDown = true;
            }
        }
    }
}
