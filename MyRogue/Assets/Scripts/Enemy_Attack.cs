using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private float timeBetweenAttack; // Время между выстрелами
    public float starttimeBetweenAttack; // Начальное время между выстрелами  

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    anim.SetTrigger("attack");
            //    Collider2D[] enemies = Physics2D
            //}
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
