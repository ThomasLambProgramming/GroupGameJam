using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero;
    public float moveSpeed = 10f;
    private Rigidbody enemyRb = null;
    public float wallStayTimer = 1.5f;
    private float wallTimer = 0;
    private bool inWall = false;
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
    private void Update()
    {
        if(inWall)
        {
            wallTimer += Time.deltaTime;
            if (wallTimer > wallStayTimer)
            {
                Destroy(gameObject);
            }
        }
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            inWall = true;
        }
        
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            inWall = false;
            wallTimer = 0;
        }
    }
    public IEnumerator TurnOnCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = true;
    }
}
