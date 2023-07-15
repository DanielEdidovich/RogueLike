using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private AddRoom room; // Ссылка на скрипт AddRoom
    private float timeBetweenAttack; // Время между атаками
    public float starttimeBetweenAttack; // Начальное время между атаками

    public Transform attackPos; // Позиция для атаки
    public LayerMask enemy; // Слой врагов
    public float attackRange; // Радиус атаки
    public int damage; // Урон от атаки
    private Animator anim; // Компонент анимации
    private Transform player; // Трансформ игрока

    public int health; // Здоровье врага
    public float speed; // Скорость перемещения
    public GameObject deathEffect; // Эффект смерти врага
    public GameObject hitEffect; // Эффект "хита" героя

    private bool facingRight = true; // Переменная, указывающая направление взгляда врага

    private void Start()
    {
        anim = GetComponent<Animator>(); // Получаем компонент анимации из объекта
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу "Player"
        room = GetComponentInParent<AddRoom>(); // Получаем скрипт AddRoom из родительского объекта
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (room != null)
            {
                room.enemies.Remove(gameObject);
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity); // Создаем эффект смерти врага
            Destroy(gameObject); // Уничтожаем объект врага
            //Destroy(transform.parent.gameObject);
            //if (room != null)
            //{
            room.enemies.Remove(gameObject);
            

            //}
            //room.enemies.Remove(gameObject); // Удаляем врага из списка врагов в скрипте AddRoom
        }

        Vector2 direction = (player.position - transform.position).normalized; // Вычисляем направление к игроку
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime; // Вычисляем новую позицию

        if (newPosition.x > transform.position.x && facingRight) // Если новая позиция слева от текущей и враг смотрит вправо
        {
            Flip(); // Поворачиваем врага
        }
        else if (newPosition.x < transform.position.x && !facingRight) // Если новая позиция справа от текущей и враг смотрит влево
        {
            Flip(); // Поворачиваем врага
        }

        transform.position = newPosition; // Обновляем позицию врага
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Уменьшаем здоровье врага
    }

    private void Flip()
    {
        facingRight = !facingRight; // Инвертируем переменную направления взгляда
        Vector3 scaler = transform.localScale; // Получаем текущий масштаб объекта
        scaler.x *= -1; // Инвертируем масштаб по оси X
        transform.localScale = scaler; // Применяем новый масштаб
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBetweenAttack <= 0)
            {
                anim.SetTrigger("enemy_attack"); // Устанавливаем триггер анимации атаки
                timeBetweenAttack = starttimeBetweenAttack; // Сбрасываем таймер атаки
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime; // Уменьшаем таймер атаки
            }
        }
    }

    public void OnEnemyAttack()
    {
        Instantiate(hitEffect, player.transform.position, Quaternion.identity); // Создаем эффект "хита" героя
        player.GetComponent<Player>().TakeDamage(damage); // Наносим урон герою
        timeBetweenAttack = starttimeBetweenAttack; // Сбрасываем таймер атаки
    }
}




