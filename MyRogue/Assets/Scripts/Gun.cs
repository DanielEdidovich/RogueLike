using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource gunshotAudioSource;

    public float rotationSpeed ; // Скорость вращения оружия
    public GameObject bullet; // Префаб снаряда
    public Transform shotPoint; // Точка, откуда будет выпущен снаряд

    private Quaternion initialRotation; // Начальное вращение оружия
    private float timeBetweenShots; // Время между выстрелами
    public float startTimeBetweenShots; // Начальное время между выстрелами 

    private void Start()
    {
        initialRotation = transform.rotation; // Сохраняем начальное вращение
        timeBetweenShots = startTimeBetweenShots; // Устанавливаем начальное время между выстрелами
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && timeBetweenShots <= 0) // Если нажата левая кнопка мыши и прошло достаточное время с последнего выстрела
        {
            Vector3 mousePosition = Input.mousePosition; // Получаем позицию указателя мыши
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position); // Преобразуем позицию оружия в координаты экрана
            Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y); // Рассчитываем смещение между указателем мыши и оружием
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; // Рассчитываем угол между смещением и осью X
            Quaternion desiredRotation = Quaternion.Euler(0f, 0f, angle); // Создаем желаемое вращение на основе угла
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime); // Применяем линейную интерполяцию для плавного поворота оружия

            gunshotAudioSource = GetComponent<AudioSource>();
            gunshotAudioSource.playOnAwake = false;
            Shoot(); // Выполняем стрельбу
            timeBetweenShots = startTimeBetweenShots; // Сбрасываем время между выстрелами
        }
        else
        {
            transform.rotation = initialRotation; // Возвращаем оружие к начальному вращению
            timeBetweenShots -= Time.deltaTime; // Уменьшаем время между выстрелами
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);

        gunshotAudioSource.Play();
        // Дополнительный код для стрельбы
        Instantiate(bullet, shotPoint.position, transform.rotation); // Создаем снаряд в позиции точки выстрела с текущим вращением оружия
    }
}




