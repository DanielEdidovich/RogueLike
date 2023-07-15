using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 changeCameraPos; // Изменение позиции камеры при смене комнаты
    public Vector3 playerChangePos; // Изменение позиции игрока при смене комнаты
    private Camera Cam; // Ссылка на компонент Camera

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.GetComponent<Camera>(); // Получаем компонент Camera из главной камеры
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Если столкнулись с объектом игрока
        {
            other.transform.position += playerChangePos; // Изменяем позицию игрока
            Cam.transform.position += changeCameraPos; // Изменяем позицию камеры
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

