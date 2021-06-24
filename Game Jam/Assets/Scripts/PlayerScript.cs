using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerHealth = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            //Game over
            transform.position = new Vector3(0, 6, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            playerHealth--;
        }
    }
}
