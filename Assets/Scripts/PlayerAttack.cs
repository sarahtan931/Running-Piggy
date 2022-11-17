using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Animator animator;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public Collider2D colliderFeet;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        animator.SetBool("IsHitting", false);
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetBool("IsHitting", true);

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);           
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().Kill();
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
          
    }
}
