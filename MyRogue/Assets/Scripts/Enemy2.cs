using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private AddRoom room; // ������ �� ������ AddRoom
    private float timeBetweenAttack; // ����� ����� �������
    public float starttimeBetweenAttack; // ��������� ����� ����� �������

    public Transform attackPos; // ������� ��� �����
    public LayerMask enemy; // ���� ������
    public float attackRange; // ������ �����
    public int damage; // ���� �� �����
    private Animator anim; // ��������� ��������
    private Transform player; // ��������� ������

    public int health; // �������� �����
    public float speed; // �������� �����������
    public GameObject deathEffect; // ������ ������ �����
    public GameObject hitEffect; // ������ "����" �����

    private bool facingRight = true; // ����������, ����������� ����������� ������� �����

    private void Start()
    {
        anim = GetComponent<Animator>(); // �������� ��������� �������� �� �������
        player = GameObject.FindGameObjectWithTag("Player").transform; // ������� ������ �� ���� "Player"
        room = GetComponentInParent<AddRoom>(); // �������� ������ AddRoom �� ������������� �������
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (room != null)
            {
                room.enemies.Remove(gameObject);
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity); // ������� ������ ������ �����
            Destroy(gameObject); // ���������� ������ �����
            //Destroy(transform.parent.gameObject);
            //if (room != null)
            //{
            room.enemies.Remove(gameObject);
            

            //}
            //room.enemies.Remove(gameObject); // ������� ����� �� ������ ������ � ������� AddRoom
        }

        Vector2 direction = (player.position - transform.position).normalized; // ��������� ����������� � ������
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime; // ��������� ����� �������

        if (newPosition.x > transform.position.x && facingRight) // ���� ����� ������� ����� �� ������� � ���� ������� ������
        {
            Flip(); // ������������ �����
        }
        else if (newPosition.x < transform.position.x && !facingRight) // ���� ����� ������� ������ �� ������� � ���� ������� �����
        {
            Flip(); // ������������ �����
        }

        transform.position = newPosition; // ��������� ������� �����
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ��������� �������� �����
    }

    private void Flip()
    {
        facingRight = !facingRight; // ����������� ���������� ����������� �������
        Vector3 scaler = transform.localScale; // �������� ������� ������� �������
        scaler.x *= -1; // ����������� ������� �� ��� X
        transform.localScale = scaler; // ��������� ����� �������
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBetweenAttack <= 0)
            {
                anim.SetTrigger("enemy_attack"); // ������������� ������� �������� �����
                timeBetweenAttack = starttimeBetweenAttack; // ���������� ������ �����
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime; // ��������� ������ �����
            }
        }
    }

    public void OnEnemyAttack()
    {
        Instantiate(hitEffect, player.transform.position, Quaternion.identity); // ������� ������ "����" �����
        player.GetComponent<Player>().TakeDamage(damage); // ������� ���� �����
        timeBetweenAttack = starttimeBetweenAttack; // ���������� ������ �����
    }
}




