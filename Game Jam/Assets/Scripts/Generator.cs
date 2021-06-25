using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //since the generator will kill half of the map (depending on the sides)
    //all we need to know is if its horizontal or vertical
    private GamestateManager gameManager = null;
    public float heightOfHalf = 4f;
    public float widthOfHalf = 4f;

    private bool isOn = false;
    public float onTimer = 2f;
    private float timerForStop = 0;
    public Vector3 middlePointOfVertical = Vector3.zero;
    public Vector3 middlePointOfHorizontal = Vector3.zero;
    //public ParticleSystem playerPressedParticle = null;
    //public ParticleSystem enemyGeneratorDeath = null;
    private PlayerScript player = null;
    private EnemySpawner spawner = null;
    private Light chargeLight = null;
    public void Start()
    {
        chargeLight = GetComponentInChildren<Light>();
        player = FindObjectOfType<PlayerScript>();
        spawner = FindObjectOfType<EnemySpawner>();
        gameManager = FindObjectOfType<GamestateManager>();
    }
    public void SwitchUsed()
    {
        if (isOn)
        {
            isOn = false;
            timerForStop = 0;
            spawner.GeneratorOff();
        }
    }
    public void Update()
    {
        if (player.PlayerCharged())
        {
            chargeLight.enabled = true;
        }
        if (isOn)
        {
            timerForStop += Time.deltaTime;
            if (timerForStop > onTimer)
            {
                isOn = false;
                timerForStop = 0;
                spawner.GeneratorOff();
            }
            Collider[] colliders;
            if (gameManager.isHorizontal())
            {
                colliders = Physics.OverlapBox(middlePointOfHorizontal, new Vector3(widthOfHalf, 2, heightOfHalf));
            }
            else
            {
                colliders = Physics.OverlapBox(middlePointOfVertical, new Vector3(heightOfHalf, 2, widthOfHalf));
            }
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Destroy(collider.gameObject);
                    //check for delay and etc on how we want to have enemies explode
                    //enemyGeneratorDeath.Play();
                }
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.PlayerCharged())
            {
                //playerPressedParticle.Play();
                isOn = true;
                spawner.GeneratorOn();
                player.ChargeUsed();
                chargeLight.enabled = false;
            }
        }
    }
}
