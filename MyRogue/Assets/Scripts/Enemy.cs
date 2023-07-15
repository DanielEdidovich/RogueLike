using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AddRoom room; // Ссылка на скрипт AddRoom
    public int health; // Здоровье врага
    public float speed; // Скорость перемещения
    public GameObject deathEffect; // Префаб эффекта смерти
    public float explosionDelay; // Задержка перед взрывом
    public int explosionDamage; // Урон от взрыва
    public Collider2D explosionCollider; // Коллайдер взрыва

    private Animator anim; // Компонент анимации

    private Transform player; // Трансформ игрока
    private bool facingRight = true; // Переменная, указывающая направление взгляда врага
    private bool isExploding = false; // Флаг, указывающий на то, происходит ли взрыв

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
       
        if (health <= 0 && !isExploding) // Если здоровье врага меньше или равно нулю и взрыв не происходит
        {
            
            anim.SetBool("IsBoom", true); // Устанавливаем флаг анимации взрыва
            StartCoroutine(ExplodeAfterDelay()); // Запускаем корутину взрыва после задержки
            return; // Прерываем выполнение функции
        }

        if (!isExploding) // Если взрыв не происходит
        {
            Vector2 direction = (player.position - transform.position).normalized; // Вычисляем направление к игроку
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime; // Вычисляем новую позицию

            if (newPosition.x < transform.position.x && !facingRight) // Если новая позиция слева от текущей и враг смотрит влево
            {
                Flip(); // Поворачиваем врага
            }
            else if (newPosition.x > transform.position.x && facingRight) // Если новая позиция справа от текущей и враг смотрит вправо
            {
                Flip(); // Поворачиваем врага
            }

            transform.position = newPosition; // Обновляем позицию врага
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Уменьшаем здоровье врага
    }

    private IEnumerator ExplodeAfterDelay()
    {
        isExploding = true; // Устанавливаем флаг взрыва в true
        speed = 0; // Устанавливаем скорость в 0
        yield return new WaitForSeconds(explosionDelay); // Ждем заданную задержку

        // Проверяем, находится ли игрок внутри коллайдера взрыва
        if (explosionCollider.OverlapPoint(player.position))
        {
            Player playerScript = player.GetComponent<Player>(); // Получаем компонент Player из игрока
            if (playerScript != null) // Если компонент Player существует
            {
                playerScript.TakeDamage(explosionDamage); // Наносим игроку урон от взрыва
            }
        }
        //explosionAudioSource.Play();
        Instantiate(deathEffect, transform.position, Quaternion.identity); // Создаем эффект смерти врага

        Destroy(gameObject); // Уничтожаем объект врага
        //Destroy(transform.parent.gameObject);

        room.enemies.Remove(gameObject);

    }

    private void Flip()
    {
        facingRight = !facingRight; // Инвертируем переменную направления взгляда
        Vector3 scaler = transform.localScale; // Получаем текущий масштаб объекта
        scaler.x *= -1; // Инвертируем масштаб по оси X
        transform.localScale = scaler; // Применяем новый масштаб
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Если столкнулись с игроком
        {
            anim.SetBool("IsBoom", true); // Устанавливаем флаг анимации взрыва
            StartCoroutine(ExplodeAfterDelay()); // Запускаем корутину взрыва после задержки
        }
    }
}



