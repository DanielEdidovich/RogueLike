using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 changeCameraPos; // ��������� ������� ������ ��� ����� �������
    public Vector3 playerChangePos; // ��������� ������� ������ ��� ����� �������
    private Camera Cam; // ������ �� ��������� Camera

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.GetComponent<Camera>(); // �������� ��������� Camera �� ������� ������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ���� ����������� � �������� ������
        {
            other.transform.position += playerChangePos; // �������� ������� ������
            Cam.transform.position += changeCameraPos; // �������� ������� ������
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

