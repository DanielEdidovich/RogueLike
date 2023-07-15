using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float distance; // Максимальное расстояние для лучевого выстрела
    public float speed; // Скорость пули
    public float lifeTime; // Время жизни пули
    public int damage; // Урон пули
    public LayerMask whatIsSolid; // Слой объектов, с которыми может сталкиваться пуля

    public GameObject BulletDestroyEffect; // Префаб эффекта уничтожения пули
    private GameObject instantiatedEffect; // Ссылка на созданный эффект уничтожения пули

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
                    enemy.TakeDamage(damage); // Нанести урон врагу
                }
            }
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy2 enemy2 = hitInfo.collider.GetComponentInChildren<Enemy2>();
                if (enemy2 != null)
                {
                    enemy2.TakeDamage(damage); // Нанести урон врагу
                }
            }
            DestroyBullet(); // Уничтожить пулю
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Переместить пулю вперед
    }


    public void DestroyBullet()
    {
        instantiatedEffect = Instantiate(BulletDestroyEffect, transform.position, Quaternion.identity); // Создать эффект уничтожения пули
        Destroy(gameObject); // Уничтожить саму пулю
        Destroy(instantiatedEffect, 0.5f); // Уничтожить эффект через 0.5 секунды
    }
}











