using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_1 : MonoBehaviour
{
    public float fireRate = 2.0f;
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public GameObject shot5;
    public GameObject player;
    public Transform shotSpawn;

    private GameObject shot;
    private GameController gameController;
    private int rank;
    private int[,] rankTable = { { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 2 }, { 1, 1, 1, 2, 2 }, { 1, 1, 2, 2, 3 }, { 1, 2, 2, 3, 3 }, { 2, 2, 3, 3, 3 },
        { 2, 2, 3, 3, 4 }, { 2, 3, 3, 4, 4 }, { 3, 3, 4, 4, 4 }, { 3, 3, 4, 4, 5 }, { 3, 4, 4, 5, 5 } };

    private float nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();
        else
            Debug.Log("找不到tag为GameController的对象");

        if (gameController == null)
            Debug.Log("找不到 GameController 脚本");

        rank = (int)Math.Floor(gameController.getRank() / 24);
        System.Random rd = new System.Random();
        int shotType = rankTable[rank,rd.Next(0, 5)];
        switch (shotType)
        {
            case 1: shot = shot1; break;
            case 2: shot = shot2; break;
            case 3: shot = shot3; break;
            case 4: shot = shot4; break;
            case 5: shot = shot5; break;
            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if (player != null && Time.time > nextFire)
        {
            shotSpawn.LookAt(player.transform.position);
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }
}
