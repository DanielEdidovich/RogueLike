using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public float health; // Здоровье игрока

    [Header("Speed")]
    public float speed; // Скорость перемещения игрока

    private Rigidbody2D rb; // Компонент Rigidbody2D
    private Vector2 moveInput; // Входные данные для перемещения
    private Vector2 moveVelocity; // Вектор скорости перемещения

    public GameObject wallEffect; // Префаб эффекта стены
    private bool FacingRight = true; // Переменная, указывающая направление взгляда игрока

    private static int levelCount = 1; // Счетчик уровней, статическая переменная
    private int highScoreLevel = 0; // Переменная для хранения рекорда

    public TMP_Text healthPoint; // Текстовый объект для отображения здоровья
    public TMP_Text levelText; // Текстовый объект для отображения уровня


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D из объекта игрока

        //PlayerPrefs.DeleteKey("HighScore");    // чтобы сбросить рекорд
        highScoreLevel = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("Текущий: Уровень " + levelCount);
        levelText.text = "LVL: " + levelCount;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealth(0); // Обновляем отображение здоровья
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Получаем входные данные для перемещения
        moveVelocity = moveInput.normalized * speed; // Вычисляем вектор скорости перемещения

        if (!FacingRight && moveInput.x > 0) // Если игрок смотрит влево и двигается вправо
        {
            Flip(); // Поворачиваем игрока
        }
        else if (FacingRight && moveInput.x < 0) // Если игрок смотрит вправо и двигается влево
        {
            Flip(); // Поворачиваем игрока
        }

        if (health <= 0) // Если здоровье игрока меньше или равно нулю
        {
            levelCount = 1;
            highScoreLevel = 0; // Устанавливаем уровень в 0
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
            SceneManager.LoadScene("Menu");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Уменьшаем здоровье игрока
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // Перемещаем игрока
    }

    private void Flip()
    {
        FacingRight = !FacingRight; // Инвертируем переменную направления взгляда
        Vector3 Scaler = transform.localScale; // Получаем текущий масштаб объекта
        Scaler.x *= -1; // Инвертируем масштаб по оси X
        transform.localScale = Scaler; // Применяем новый масштаб
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("potion")) // Если столкнулись с объектом с тегом "potion"
        {
            ChangeHealth(5); // Изменяем здоровье игрока
            Destroy(other.gameObject); // Уничтожаем объект
        }
        else if (other.CompareTag("Spike")) // Если столкнулись с объектом с тегом "Spike"
        {
            TakeDamage(2); // Наносим урон игроку
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Door")) // Если находимся в триггере объекта с тегом "Door"
        {
            Instantiate(wallEffect, other.transform.position, Quaternion.identity); // Создаем эффект стены
            other.gameObject.SetActive(false); // Делаем объект неактивным
            levelCount++;
            levelText.text = "LVL: " + levelCount;
            // Записываем текущий уровень в рекорды
            if (levelCount > highScoreLevel)
            {
                highScoreLevel = levelCount;
                PlayerPrefs.SetInt("HighScore", highScoreLevel);
                Debug.Log("Новый рекорд: Уровень " + highScoreLevel);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
        }
    }



    public void ChangeHealth(int healthValue)
    {
        health += healthValue; // Изменяем здоровье игрока
        healthPoint.text = "HP: " + health; // Обновляем текстовое поле с отображением здоровья
    }
}

