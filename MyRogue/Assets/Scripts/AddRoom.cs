//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AddRoom : MonoBehaviour
//{
//    [Header("Walls")]
//    public GameObject[] walls; // ������ ���� � �������
//    public GameObject wallEffect; // ������ ������� ���������� �����
//    public GameObject door; // �����

//    [Header("Enemies")]
//    public GameObject[] enemyTypes; // ������ ����� ������
//    public Transform[] enemySpawners; // ������ ����� ��������� ������

//    [Header("PowerUp")]
//    public GameObject healthPotion; // ������ �������

//    [HideInInspector] public List<GameObject> enemies; // ������ ������ � �������

//    private RoomVariants variants; // ������ �� ������ RoomVariants
//    private bool spawned; // ����, �����������, ���� �� ����� ��� �������
//    private bool WallDestroyed; // ����, �����������, ���� �� ����� ���������

//    // Start is called before the first frame update
//    void Start()
//    {
//        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // ������� ������ � ����� "Rooms" � �������� ��� ������ RoomVariants
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player") && !spawned) // ���� ����������� � �������� ������ � ����� ��� �� �������
//        {
//            spawned = true; // ������������� ���� �������� ������ � true

//            foreach (Transform spawner in enemySpawners) // ��� ������ ����� ��������� �����
//            {
//                int rand = Random.Range(0, 11); // ���������� ��������� ����� �� 0 �� 10
//                if (rand < 8) // ���� ��������� ����� ������ 8
//                {
//                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; // �������� ��������� ��� ����� �� ������� enemyTypes
//                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject; // ������� �����
//                    enemy.transform.parent = transform; // ������������� ������������ ������ ��� �����
//                    enemies.Add(enemy); // ��������� ����� � ������ ������
//                }
//                else if (rand > 8) // ���� ��������� ����� ������ 8
//                {
//                    Instantiate(healthPotion, spawner.position, Quaternion.identity); // ������� �������
//                }
//            }
//            StartCoroutine(CheckEnemies()); // ��������� �������� ��� �������� ������
//        }
//    }

//    IEnumerator CheckEnemies()
//    {

//        yield return new WaitForSeconds(1f); // ���� 1 �������
//        yield return new WaitUntil(() => enemies.Count == 0); // ����, ���� ������ ������ ������ ������
//        DestroyWalls(); // ���������� �����
//    }

//    public void DestroyWalls()
//    {
//        foreach (GameObject wall in walls) // ��� ������ �����
//        {
//            if (wall != null && wall.transform.childCount != 0) // ���� ����� ���������� � ����� �������� �������
//            {
//                Instantiate(wallEffect, wall.transform.position, Quaternion.identity); // ������� ������ ���������� �����
//                Destroy(wall); // ���������� �����
//            }
//        }
//        WallDestroyed = true; // ������������� ���� ���������� ���� � true
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if (WallDestroyed && other.CompareTag("Wall")) // ���� ����� ��������� � ����������� �� ������
//        {
//            Destroy(other.gameObject); // ���������� �����
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
    public GameObject[] walls; // ������ ���� � �������
    public GameObject wallEffect; // ������ ������� ���������� �����
    public GameObject door; // �����

    [Header("Enemies")]
    public GameObject[] enemyTypes; // ������ ����� ������
    public Transform[] enemySpawners; // ������ ����� ��������� ������

    [Header("PowerUp")]
    public GameObject healthPotion; // ������ �������

    [HideInInspector] public List<GameObject> enemies; // ������ ������ � �������

    private RoomVariants variants; // ������ �� ������ RoomVariants
    private bool spawned; // ����, �����������, ���� �� ����� ��� �������
    private bool wallDestroyed; // ����, �����������, ���� �� ����� ���������
    public bool enemySpawnersActive = true;

    // Start is called before the first frame update
    private void Awake()
    {    
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // ������� ������ � ����� "Rooms" � �������� ��� ������ RoomVariants    
    }
    private void Start()
    {
        variants.rooms.Add(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && !spawned) // ���� ����������� � �������� ������ � ����� ��� �� �������
    //    {
    //        spawned = true; // ������������� ���� �������� ������ � true

    //        foreach (Transform spawner in enemySpawners) // ��� ������ ����� ��������� �����
    //        {
    //            int rand = Random.Range(0, 11); // ���������� ��������� ����� �� 0 �� 10
    //            if (rand < 8) // ���� ��������� ����� ������ 8
    //            {
    //                GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; // �������� ��������� ��� ����� �� ������� enemyTypes
    //                GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject; // ������� �����
    //                enemy.transform.parent = transform; // ������������� ������������ ������ ��� �����

    //                enemies.Add(enemy); // ��������� ����� � ������ ������

    //            }
    //            else if (rand > 8) // ���� ��������� ����� ������ 8
    //            {
    //                Instantiate(healthPotion, spawner.position, Quaternion.identity); // ������� �������
    //            }
    //        }
    //        StartCoroutine(CheckEnemies()); // ��������� �������� ��� �������� ������
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned) // ���� ����������� � �������� ������ � ����� ��� �� �������
        {
            spawned = true; // ������������� ���� �������� ������ � true

            foreach (Transform spawner in enemySpawners) // ��� ������ ����� ��������� �����
            {
                if (spawner.gameObject.activeSelf) // ��������� ���������� �������� �����
                {
                    int rand = Random.Range(0, 11); // ���������� ��������� ����� �� 0 �� 10
                    if (rand < 8) // ���� ��������� ����� ������ 8
                    {
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)]; // �������� ��������� ��� ����� �� ������� enemyTypes
                        GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject; // ������� �����
                        enemy.transform.parent = transform; // ������������� ������������ ������ ��� �����

                        enemies.Add(enemy); // ��������� ����� � ������ ������
                    }
                    else if (rand > 8) // ���� ��������� ����� ������ 8
                    {
                        Instantiate(healthPotion, spawner.position, Quaternion.identity); // ������� �������
                    }
                }
            }
            StartCoroutine(CheckEnemies()); // ��������� �������� ��� �������� ������
        }
    }



    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f); // ���� 1 �������
        yield return new WaitUntil(() => enemies.Count == 0); // ����, ���� ������ ������ ������ ������
        DestroyWalls(); // ���������� �����
    }

    public void DestroyWalls()
    {
        foreach (GameObject wall in walls) // ��� ������ �����
        {
            if (wall != null && wall.transform.childCount != 0) // ���� ����� ���������� � ����� �������� �������
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity); // ������� ������ ���������� �����
                Destroy(wall); // ���������� �����
            }
        }
        wallDestroyed = true; // ������������� ���� ���������� ���� � true

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (wallDestroyed && other.CompareTag("Wall")) // ���� ����� ��������� � ����������� �� ������
        {
            Destroy(other.gameObject); // ���������� �����
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
