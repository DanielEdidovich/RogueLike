//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RoomSpawner : MonoBehaviour
//{
//    public Direction direction; // ����������� �������
//    public enum Direction
//    {
//        Top,
//        Bottom,
//        Left,
//        Right,
//        None
//    }
//    private RoomVariants variants; // ������ �� ������ RoomVariants
//    private int rand; // ��������� �����
//    private bool spawned = false; // ����, �����������, ���� �� ��� ������� �������
//    private float waitTime = 3f; // ����� �������� ����� ��������� �������

//    // Start is called before the first frame update
//    void Start()
//    {
//        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // ������� ������ � ����� "Rooms" � �������� ��� ������ RoomVariants
//        Destroy(gameObject, waitTime); // ���������� ������ ����� �������� �����
//        Invoke("Spawn", 0.2f); // ��������� ����� Spawn() ����� 0.2 �������
//    }

//    public void Spawn()
//    {
//        if (!spawned) // ���� ������� ��� �� �������
//        {
//            if (direction == Direction.Top) // ���� ����������� ������� �����
//            {
//                rand = Random.Range(0, variants.topRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� topRooms � ������� RoomVariants
//                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation); // ������� �������
//            }
//            else if (direction == Direction.Bottom) // ���� ����������� ������� ����
//            {
//                rand = Random.Range(0, variants.bottomRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� bottomRooms � ������� RoomVariants
//                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation); // ������� �������
//            }
//            else if (direction == Direction.Right) // ���� ����������� ������� ������
//            {
//                rand = Random.Range(0, variants.rightRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� rightRooms � ������� RoomVariants
//                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation); // ������� �������
//            }
//            else if (direction == Direction.Left) // ���� ����������� ������� �����
//            {
//                rand = Random.Range(0, variants.leftRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� leftRooms � ������� RoomVariants
//                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation); // ������� �������
//            }
//            spawned = true; // ������������� ���� �������� ������� � true
//        }
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned) // ���� ����������� � ������ ������� � ������� �� ����� ��� �������
//        {
//            Destroy(gameObject); // ���������� ������
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
    public Direction direction; // ����������� �������
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariants variants; // ������ �� ������ RoomVariants
    private int rand; // ��������� �����
    private bool spawned = false; // ����, �����������, ���� �� ��� ������� �������
    private float waitTime = 3f; // ����� �������� ����� ��������� �������

    // Start is called before the first frame update
    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); // ������� ������ � ����� "Rooms" � �������� ��� ������ RoomVariants
        Destroy(gameObject, waitTime); // ���������� ������ ����� �������� �����
        Invoke("Spawn", 0.2f); // ��������� ����� Spawn() ����� 0.2 �������
    }

    public void Spawn()
    {
        if (!spawned) // ���� ������� ��� �� �������
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // ��������� �������� ������ ����� ������

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("RoomPoint") && collider.gameObject != gameObject) // ���� ������� ����� �������, �� ��� �� ������� ������
                {
                    RoomSpawner otherSpawner = collider.GetComponent<RoomSpawner>();
                    if (otherSpawner != null && otherSpawner.spawned) // ���� ������� �� ����� ��� �������
                    {
                        spawned = true; // ������������� ���� �������� ������� � true
                        return;
                    }
                }
            }

            if (direction == Direction.Top) // ���� ����������� ������� �����
            {
                rand = Random.Range(0, variants.topRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� topRooms � ������� RoomVariants
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation); // ������� �������
            }
            else if (direction == Direction.Bottom) // ���� ����������� ������� ����
            {
                rand = Random.Range(0, variants.bottomRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� bottomRooms � ������� RoomVariants
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation); // ������� �������
            }
            else if (direction == Direction.Right) // ���� ����������� ������� ������
            {
                rand = Random.Range(0, variants.rightRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� rightRooms � ������� RoomVariants
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation); // ������� �������
            }
            else if (direction == Direction.Left) // ���� ����������� ������� �����
            {
                rand = Random.Range(0, variants.leftRooms.Length); // ���������� ��������� ����� ��� ������ ������� �� ������� leftRooms � ������� RoomVariants
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation); // ������� �������
            }

            spawned = true; // ������������� ���� �������� ������� � true
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned) // ���� ����������� � ������ ������� � ������� �� ����� ��� �������
        {
            Destroy(gameObject); // ���������� ������
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}









