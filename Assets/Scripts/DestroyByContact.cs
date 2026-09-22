using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject itemDrop;
    public Transform shotSpawn;
    public int scoreValue; // how many score will be added when an asteroid's shot
    public int enemyLife;
    private GameController gameController; // GameController script instance

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
            Instantiate(explosion, transform.position, transform.rotation);
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
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameController.AddScore(5);
            Destroy(other.gameObject);
            enemyLife--;
            if (enemyLife <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                gameController.AddScore(scoreValue);
                Instantiate(itemDrop, shotSpawn.position, shotSpawn.rotation);
                Destroy(gameObject);
            }
        }
    }
}
