//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RoomSpawner : MonoBehaviour
//{
//    public Direction direction; // Направление комнаты
//    public enum Direction
//    {
//        Top,
//        Bottom,
//        Left,
//        Right,
//        None
//    }
//    private RoomVariants variants; // Ссылка на скрипт RoomVariants
//    private int rand; // Случайное число
//    private bool spawned = false; // Флаг, указывающий, была ли уже создана комната
//    private float waitTime = 3f; // Время ожидания перед удалением объекта

//    // Start is called before the first frame update
//    void Start()
//    {
//        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // Находим объект с тегом "Rooms" и получаем его скрипт RoomVariants
//        Destroy(gameObject, waitTime); // Уничтожаем объект через заданное время
//        Invoke("Spawn", 0.2f); // Запускаем метод Spawn() через 0.2 секунды
//    }

//    public void Spawn()
//    {
//        if (!spawned) // Если комната еще не создана
//        {
//            if (direction == Direction.Top) // Если направление комнаты вверх
//            {
//                rand = Random.Range(0, variants.topRooms.Length); // Генерируем случайное число для выбора комнаты из массива topRooms в скрипте RoomVariants
//                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation); // Создаем комнату
//            }
//            else if (direction == Direction.Bottom) // Если направление комнаты вниз
//            {
//                rand = Random.Range(0, variants.bottomRooms.Length); // Генерируем случайное число для выбора комнаты из массива bottomRooms в скрипте RoomVariants
//                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation); // Создаем комнату
//            }
//            else if (direction == Direction.Right) // Если направление комнаты вправо
//            {
//                rand = Random.Range(0, variants.rightRooms.Length); // Генерируем случайное число для выбора комнаты из массива rightRooms в скрипте RoomVariants
//                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation); // Создаем комнату
//            }
//            else if (direction == Direction.Left) // Если направление комнаты влево
//            {
//                rand = Random.Range(0, variants.leftRooms.Length); // Генерируем случайное число для выбора комнаты из массива leftRooms в скрипте RoomVariants
//                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation); // Создаем комнату
//            }
//            spawned = true; // Устанавливаем флаг создания комнаты в true
//        }
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned) // Если столкнулись с точкой комнаты и комната на точке уже создана
//        {
//            Destroy(gameObject); // Уничтожаем объект
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction; // Направление комнаты
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariants variants; // Ссылка на скрипт RoomVariants
    private int rand; // Случайное число
    private bool spawned = false; // Флаг, указывающий, была ли уже создана комната
    private float waitTime = 3f; // Время ожидания перед удалением объекта

    // Start is called before the first frame update
    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // Находим объект с тегом "Rooms" и получаем его скрипт RoomVariants
        Destroy(gameObject, waitTime); // Уничтожаем объект через заданное время
        Invoke("Spawn", 0.2f); // Запускаем метод Spawn() через 0.2 секунды
    }

    public void Spawn()
    {
        if (!spawned) // Если комната еще не создана
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Проверяем коллизии вокруг точки спавна

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("RoomPoint") && collider.gameObject != gameObject) // Если найдена точка комнаты, но это не текущий объект
                {
                    RoomSpawner otherSpawner = collider.GetComponent<RoomSpawner>();
                    if (otherSpawner != null && otherSpawner.spawned) // Если комната на точке уже создана
                    {
                        spawned = true; // Устанавливаем флаг создания комнаты в true
                        return;
                    }
                }
            }

            if (direction == Direction.Top) // Если направление комнаты вверх
            {
                rand = Random.Range(0, variants.topRooms.Length); // Генерируем случайное число для выбора комнаты из массива topRooms в скрипте RoomVariants
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation); // Создаем комнату
            }
            else if (direction == Direction.Bottom) // Если направление комнаты вниз
            {
                rand = Random.Range(0, variants.bottomRooms.Length); // Генерируем случайное число для выбора комнаты из массива bottomRooms в скрипте RoomVariants
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation); // Создаем комнату
            }
            else if (direction == Direction.Right) // Если направление комнаты вправо
            {
                rand = Random.Range(0, variants.rightRooms.Length); // Генерируем случайное число для выбора комнаты из массива rightRooms в скрипте RoomVariants
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation); // Создаем комнату
            }
            else if (direction == Direction.Left) // Если направление комнаты влево
            {
                rand = Random.Range(0, variants.leftRooms.Length); // Генерируем случайное число для выбора комнаты из массива leftRooms в скрипте RoomVariants
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation); // Создаем комнату
            }

            spawned = true; // Устанавливаем флаг создания комнаты в true
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned) // Если столкнулись с точкой комнаты и комната на точке уже создана
        {
            Destroy(gameObject); // Уничтожаем объект
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}









