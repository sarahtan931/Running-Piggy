using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkull : MonoBehaviour
{

    public Animator animator;

    public Transform attackPos;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public GameObject enemy;

    void Update()
    {
        animator.SetBool("IsHitting", false);
           
       Collider2D[] player = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
       for (int i = 0; i < player.Length; i++)
       {
            animator.SetBool("IsHitting", true);
            dieing(player[i].GetComponent<PlayerMovement>());
        }
             
    }

    IEnumerator dieing(PlayerMovement player)
    {
        yield return new WaitForSeconds(1);
        player.Kill();
  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            Destroy(enemy);
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

    }
}
