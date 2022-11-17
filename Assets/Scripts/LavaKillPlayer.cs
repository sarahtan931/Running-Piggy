using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaKillPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPosition;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(Coroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            player.transform.position = respawnPosition.position;
        }
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(1);
        player.transform.position = respawnPosition.position;
    }
}
