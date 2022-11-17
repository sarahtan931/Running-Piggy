using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBunny : MonoBehaviour
{
    public float min = 1f;
    public float max = 2f;
    private float bunnyMin;
    private float bunnyMax;
    private Vector3 localScale;
    public GameObject enemy;
    private float left;
    private float right;
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            Destroy(enemy);
        }
    }

    void Start()
    {
        bunnyMin = transform.position.x;
        bunnyMax = transform.position.x + max;
        localScale = transform.localScale;
        left = localScale.x;
        right = localScale.x * -1;
        
    }


    void Update()
    {
        var lastVal = transform.position.x;
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, bunnyMax - bunnyMin) + bunnyMin, transform.position.y, transform.position.z);
  
        if (transform.position.x < lastVal)
         {
            transform.localScale = new Vector3(left, transform.localScale.y, transform.localScale.z);
          
         }
          else if (transform.position.x > lastVal) {
            transform.localScale = new Vector3(right, transform.localScale.y, transform.localScale.z);
        }
    }
}