using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AddRoom room; // ������ �� ������ AddRoom
    public int health; // �������� �����
    public float speed; // �������� �����������
    public GameObject deathEffect; // ������ ������� ������
    public float explosionDelay; // �������� ����� �������
    public int explosionDamage; // ���� �� ������
    public Collider2D explosionCollider; // ��������� ������

    private Animator anim; // ��������� ��������

    private Transform player; // ��������� ������
    private bool facingRight = true; // ����������, ����������� ����������� ������� �����
    private bool isExploding = false; // ����, ����������� �� ��, ���������� �� �����

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        room = GetComponentInParent<AddRoom>();

        //explosionAudioSource = GetComponent<AudioSource>();
        //explosionAudioSource.clip = explosionSound;
    }

    private void Update()
    {
       
        if (health <= 0 && !isExploding) // ���� �������� ����� ������ ��� ����� ���� � ����� �� ����������
        {
            
            anim.SetBool("IsBoom", true); // ������������� ���� �������� ������
            StartCoroutine(ExplodeAfterDelay()); // ��������� �������� ������ ����� ��������
            return; // ��������� ���������� �������
        }

        if (!isExploding) // ���� ����� �� ����������
        {
            Vector2 direction = (player.position - transform.position).normalized; // ��������� ����������� � ������
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime; // ��������� ����� �������

            if (newPosition.x < transform.position.x && !facingRight) // ���� ����� ������� ����� �� ������� � ���� ������� �����
            {
                Flip(); // ������������ �����
            }
            else if (newPosition.x > transform.position.x && facingRight) // ���� ����� ������� ������ �� ������� � ���� ������� ������
            {
                Flip(); // ������������ �����
            }

            transform.position = newPosition; // ��������� ������� �����
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ��������� �������� �����
    }

    private IEnumerator ExplodeAfterDelay()
    {
        isExploding = true; // ������������� ���� ������ � true
        speed = 0; // ������������� �������� � 0
        yield return new WaitForSeconds(explosionDelay); // ���� �������� ��������

        // ���������, ��������� �� ����� ������ ���������� ������
        if (explosionCollider.OverlapPoint(player.position))
        {
            Player playerScript = player.GetComponent<Player>(); // �������� ��������� Player �� ������
            if (playerScript != null) // ���� ��������� Player ����������
            {
                playerScript.TakeDamage(explosionDamage); // ������� ������ ���� �� ������
            }
        }
        //explosionAudioSource.Play();
        Instantiate(deathEffect, transform.position, Quaternion.identity); // ������� ������ ������ �����

        Destroy(gameObject); // ���������� ������ �����
        //Destroy(transform.parent.gameObject);

        room.enemies.Remove(gameObject);

    }

    private void Flip()
    {
        facingRight = !facingRight; // ����������� ���������� ����������� �������
        Vector3 scaler = transform.localScale; // �������� ������� ������� �������
        scaler.x *= -1; // ����������� ������� �� ��� X
        transform.localScale = scaler; // ��������� ����� �������
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ���� ����������� � �������
        {
            anim.SetBool("IsBoom", true); // ������������� ���� �������� ������
            StartCoroutine(ExplodeAfterDelay()); // ��������� �������� ������ ����� ��������
        }
    }
}



