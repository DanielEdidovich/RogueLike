using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource gunshotAudioSource;

    public float rotationSpeed ; // �������� �������� ������
    public GameObject bullet; // ������ �������
    public Transform shotPoint; // �����, ������ ����� ������� ������

    private Quaternion initialRotation; // ��������� �������� ������
    private float timeBetweenShots; // ����� ����� ����������
    public float startTimeBetweenShots; // ��������� ����� ����� ���������� 

    private void Start()
    {
        initialRotation = transform.rotation; // ��������� ��������� ��������
        timeBetweenShots = startTimeBetweenShots; // ������������� ��������� ����� ����� ����������
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && timeBetweenShots <= 0) // ���� ������ ����� ������ ���� � ������ ����������� ����� � ���������� ��������
        {
            Vector3 mousePosition = Input.mousePosition; // �������� ������� ��������� ����
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position); // ����������� ������� ������ � ���������� ������
            Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y); // ������������ �������� ����� ���������� ���� � �������
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; // ������������ ���� ����� ��������� � ���� X
            Quaternion desiredRotation = Quaternion.Euler(0f, 0f, angle); // ������� �������� �������� �� ������ ����
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime); // ��������� �������� ������������ ��� �������� �������� ������

            gunshotAudioSource = GetComponent<AudioSource>();
            gunshotAudioSource.playOnAwake = false;
            Shoot(); // ��������� ��������
            timeBetweenShots = startTimeBetweenShots; // ���������� ����� ����� ����������
        }
        else
        {
            transform.rotation = initialRotation; // ���������� ������ � ���������� ��������
            timeBetweenShots -= Time.deltaTime; // ��������� ����� ����� ����������
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);

        gunshotAudioSource.Play();
        // �������������� ��� ��� ��������
        Instantiate(bullet, shotPoint.position, transform.rotation); // ������� ������ � ������� ����� �������� � ������� ��������� ������
    }
}




