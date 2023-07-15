using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public float health; // �������� ������

    [Header("Speed")]
    public float speed; // �������� ����������� ������

    private Rigidbody2D rb; // ��������� Rigidbody2D
    private Vector2 moveInput; // ������� ������ ��� �����������
    private Vector2 moveVelocity; // ������ �������� �����������

    public GameObject wallEffect; // ������ ������� �����
    private bool FacingRight = true; // ����������, ����������� ����������� ������� ������

    private static int levelCount = 1; // ������� �������, ����������� ����������
    private int highScoreLevel = 0; // ���������� ��� �������� �������

    public TMP_Text healthPoint; // ��������� ������ ��� ����������� ��������
    public TMP_Text levelText; // ��������� ������ ��� ����������� ������


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D �� ������� ������

        //PlayerPrefs.DeleteKey("HighScore");    // ����� �������� ������
        highScoreLevel = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("�������: ������� " + levelCount);
        levelText.text = "LVL: " + levelCount;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealth(0); // ��������� ����������� ��������
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // �������� ������� ������ ��� �����������
        moveVelocity = moveInput.normalized * speed; // ��������� ������ �������� �����������

        if (!FacingRight && moveInput.x > 0) // ���� ����� ������� ����� � ��������� ������
        {
            Flip(); // ������������ ������
        }
        else if (FacingRight && moveInput.x < 0) // ���� ����� ������� ������ � ��������� �����
        {
            Flip(); // ������������ ������
        }

        if (health <= 0) // ���� �������� ������ ������ ��� ����� ����
        {
            levelCount = 1;
            highScoreLevel = 0; // ������������� ������� � 0
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������� ������� �����
            SceneManager.LoadScene("Menu");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ��������� �������� ������
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // ���������� ������
    }

    private void Flip()
    {
        FacingRight = !FacingRight; // ����������� ���������� ����������� �������
        Vector3 Scaler = transform.localScale; // �������� ������� ������� �������
        Scaler.x *= -1; // ����������� ������� �� ��� X
        transform.localScale = Scaler; // ��������� ����� �������
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("potion")) // ���� ����������� � �������� � ����� "potion"
        {
            ChangeHealth(5); // �������� �������� ������
            Destroy(other.gameObject); // ���������� ������
        }
        else if (other.CompareTag("Spike")) // ���� ����������� � �������� � ����� "Spike"
        {
            TakeDamage(2); // ������� ���� ������
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Door")) // ���� ��������� � �������� ������� � ����� "Door"
        {
            Instantiate(wallEffect, other.transform.position, Quaternion.identity); // ������� ������ �����
            other.gameObject.SetActive(false); // ������ ������ ����������
            levelCount++;
            levelText.text = "LVL: " + levelCount;
            // ���������� ������� ������� � �������
            if (levelCount > highScoreLevel)
            {
                highScoreLevel = levelCount;
                PlayerPrefs.SetInt("HighScore", highScoreLevel);
                Debug.Log("����� ������: ������� " + highScoreLevel);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������� ������� �����
        }
    }



    public void ChangeHealth(int healthValue)
    {
        health += healthValue; // �������� �������� ������
        healthPoint.text = "HP: " + health; // ��������� ��������� ���� � ������������ ��������
    }
}

