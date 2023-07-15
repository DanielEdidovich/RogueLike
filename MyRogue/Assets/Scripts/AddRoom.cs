//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AddRoom : MonoBehaviour
//{
//    [Header("Walls")]
//    public GameObject[] walls; // Массив стен в комнате
//    public GameObject wallEffect; // Префаб эффекта разрушения стены
//    public GameObject door; // Дверь

//    [Header("Enemies")]
//    public GameObject[] enemyTypes; // Массив типов врагов
//    public Transform[] enemySpawners; // Массив точек появления врагов

//    [Header("PowerUp")]
//    public GameObject healthPotion; // Префаб аптечки

//    [HideInInspector] public List<GameObject> enemies; // Список врагов в комнате

//    private RoomVariants variants; // Ссылка на скрипт RoomVariants
//    private bool spawned; // Флаг, указывающий, были ли враги уже создана
//    private bool WallDestroyed; // Флаг, указывающий, были ли стены разрушены

//    // Start is called before the first frame update
//    void Start()
//    {
//        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // Находим объект с тегом "Rooms" и получаем его скрипт RoomVariants
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player") && !spawned) // Если столкнулись с объектом игрока и враги еще не созданы
//        {
//            spawned = true; // Устанавливаем флаг создания врагов в true

//            foreach (Transform spawner in enemySpawners) // Для каждой точки появления врага
//            {
//                int rand = Random.Range(0, 11); // Генерируем случайное число от 0 до 10
//                if (rand < 8) // Если случайное число меньше 8
//                {
//                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; // Выбираем случайный тип врага из массива enemyTypes
//                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject; // Создаем врага
//                    enemy.transform.parent = transform; // Устанавливаем родительский объект для врага
//                    enemies.Add(enemy); // Добавляем врага в список врагов
//                }
//                else if (rand > 8) // Если случайное число больше 8
//                {
//                    Instantiate(healthPotion, spawner.position, Quaternion.identity); // Создаем аптечку
//                }
//            }
//            StartCoroutine(CheckEnemies()); // Запускаем корутину для проверки врагов
//        }
//    }

//    IEnumerator CheckEnemies()
//    {

//        yield return new WaitForSeconds(1f); // Ждем 1 секунду
//        yield return new WaitUntil(() => enemies.Count == 0); // Ждем, пока список врагов станет пустым
//        DestroyWalls(); // Уничтожаем стены
//    }

//    public void DestroyWalls()
//    {
//        foreach (GameObject wall in walls) // Для каждой стены
//        {
//            if (wall != null && wall.transform.childCount != 0) // Если стена существует и имеет дочерние объекты
//            {
//                Instantiate(wallEffect, wall.transform.position, Quaternion.identity); // Создаем эффект разрушения стены
//                Destroy(wall); // Уничтожаем стену
//            }
//        }
//        WallDestroyed = true; // Устанавливаем флаг разрушения стен в true
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if (WallDestroyed && other.CompareTag("Wall")) // Если стены разрушены и столкнулись со стеной
//        {
//            Destroy(other.gameObject); // Уничтожаем стену
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

public class AddRoom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls; // Массив стен в комнате
    public GameObject wallEffect; // Префаб эффекта разрушения стены
    public GameObject door; // Дверь

    [Header("Enemies")]
    public GameObject[] enemyTypes; // Массив типов врагов
    public Transform[] enemySpawners; // Массив точек появления врагов

    [Header("PowerUp")]
    public GameObject healthPotion; // Префаб аптечки

    [HideInInspector] public List<GameObject> enemies; // Список врагов в комнате

    private RoomVariants variants; // Ссылка на скрипт RoomVariants
    private bool spawned; // Флаг, указывающий, были ли враги уже созданы
    private bool wallDestroyed; // Флаг, указывающий, были ли стены разрушены
    public bool enemySpawnersActive = true;

    // Start is called before the first frame update
    private void Awake()
    {    
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // Находим объект с тегом "Rooms" и получаем его скрипт RoomVariants    
    }
    private void Start()
    {
        variants.rooms.Add(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && !spawned) // Если столкнулись с объектом игрока и враги еще не созданы
    //    {
    //        spawned = true; // Устанавливаем флаг создания врагов в true

    //        foreach (Transform spawner in enemySpawners) // Для каждой точки появления врага
    //        {
    //            int rand = Random.Range(0, 11); // Генерируем случайное число от 0 до 10
    //            if (rand < 8) // Если случайное число меньше 8
    //            {
    //                GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; // Выбираем случайный тип врага из массива enemyTypes
    //                GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject; // Создаем врага
    //                enemy.transform.parent = transform; // Устанавливаем родительский объект для врага

    //                enemies.Add(enemy); // Добавляем врага в список врагов

    //            }
    //            else if (rand > 8) // Если случайное число больше 8
    //            {
    //                Instantiate(healthPotion, spawner.position, Quaternion.identity); // Создаем аптечку
    //            }
    //        }
    //        StartCoroutine(CheckEnemies()); // Запускаем корутину для проверки врагов
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned) // Если столкнулись с объектом игрока и враги еще не созданы
        {
            spawned = true; // Устанавливаем флаг создания врагов в true

            foreach (Transform spawner in enemySpawners) // Для каждой точки появления врага
            {
                if (spawner.gameObject.activeSelf) // Проверяем активность спавнера врага
                {
                    int rand = Random.Range(0, 11); // Генерируем случайное число от 0 до 10
                    if (rand < 8) // Если случайное число меньше 8
                    {
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; // Выбираем случайный тип врага из массива enemyTypes
                        GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject; // Создаем врага
                        enemy.transform.parent = transform; // Устанавливаем родительский объект для врага

                        enemies.Add(enemy); // Добавляем врага в список врагов
                    }
                    else if (rand > 8) // Если случайное число больше 8
                    {
                        Instantiate(healthPotion, spawner.position, Quaternion.identity); // Создаем аптечку
                    }
                }
            }
            StartCoroutine(CheckEnemies()); // Запускаем корутину для проверки врагов
        }
    }



    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f); // Ждем 1 секунду
        yield return new WaitUntil(() => enemies.Count == 0); // Ждем, пока список врагов станет пустым
        DestroyWalls(); // Уничтожаем стены
    }

    public void DestroyWalls()
    {
        foreach (GameObject wall in walls) // Для каждой стены
        {
            if (wall != null && wall.transform.childCount != 0) // Если стена существует и имеет дочерние объекты
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity); // Создаем эффект разрушения стены
                Destroy(wall); // Уничтожаем стену
            }
        }
        wallDestroyed = true; // Устанавливаем флаг разрушения стен в true

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (wallDestroyed && other.CompareTag("Wall")) // Если стены разрушены и столкнулись со стеной
        {
            Destroy(other.gameObject); // Уничтожаем стену
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
