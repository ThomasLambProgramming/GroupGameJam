using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero;
    public float moveSpeed = 10f;
    private Rigidbody enemyRb = null;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyRb.velocity = moveDirection * moveSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            ContactPoint contactPoint = collision.GetContact(0);
            moveDirection = Vector3.Reflect(moveDirection, contactPoint.normal).normalized;
        }
        
    }
    public IEnumerator TurnOnCollider()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SphereCollider>().enabled = true;
    }
}
