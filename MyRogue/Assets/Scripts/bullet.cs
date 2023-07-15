using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float distance; // ������������ ���������� ��� �������� ��������
    public float speed; // �������� ����
    public float lifeTime; // ����� ����� ����
    public int damage; // ���� ����
    public LayerMask whatIsSolid; // ���� ��������, � �������� ����� ������������ ����

    public GameObject BulletDestroyEffect; // ������ ������� ����������� ����
    private GameObject instantiatedEffect; // ������ �� ��������� ������ ����������� ����

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponentInChildren<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage); // ������� ���� �����
                }
            }
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy2 enemy2 = hitInfo.collider.GetComponentInChildren<Enemy2>();
                if (enemy2 != null)
                {
                    enemy2.TakeDamage(damage); // ������� ���� �����
                }
            }
            DestroyBullet(); // ���������� ����
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime); // ����������� ���� ������
    }


    public void DestroyBullet()
    {
        instantiatedEffect = Instantiate(BulletDestroyEffect, transform.position, Quaternion.identity); // ������� ������ ����������� ����
        Destroy(gameObject); // ���������� ���� ����
        Destroy(instantiatedEffect, 0.5f); // ���������� ������ ����� 0.5 �������
    }
}











