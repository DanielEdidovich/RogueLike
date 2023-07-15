using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public Transform[] enemySpawners;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    [HideInInspector] public List<GameObject> rooms;
    //Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawner());
    }

    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        lastRoom.door.SetActive(true);

        lastRoom.DestroyWalls();

        // Отключаем EnemySpawners в последней комнате
        foreach (Transform spawner in lastRoom.enemySpawners)
        {
            spawner.gameObject.SetActive(false);
        }

    }
    //Update is called once per frame
    void Update()
    {

    }
}








