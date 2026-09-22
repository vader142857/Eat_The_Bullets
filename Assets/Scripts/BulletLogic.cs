using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public GameObject playerExplosion;
    public int BulletLevel = 0;
    public int scoreValue;
    private GameController gameController;

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
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boundary"))
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            if (BulletLevel > gameController.getLevel())
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(gameObject);
                if (gameController.getLife() == 0)
                {
                    gameController.GameOver();
                    Destroy(other.gameObject);
                }
                else
                {
                    gameController.ChangeLife(-1);
                    gameController.ChangeEnergy(-20);
                }
            }
            else
            {
                Destroy(gameObject);
                if(BulletLevel==0)
                    gameController.ChangeEnergy(10);
                else
                    gameController.ChangeEnergy(2);
                gameController.AddScore(scoreValue);
            }
        }
    }
}
