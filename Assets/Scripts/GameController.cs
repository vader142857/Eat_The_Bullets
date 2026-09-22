using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text lifeText;
    public Text energyText;
    public Text levelText;

    private bool gameOver;
    private bool restart;
    private int score;
    private int life;
    private int nextExtend;
    private int energy;
    private int level;
    private float rank = 0.0f;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        life = 2;
        nextExtend = 2500;
        energy = 0;
        level = 0;
        UpdateScore();
        UpdateLife();
        UpdateEnergy();
        UpdateLevel();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (rank <= 240) rank += 0.0025f;

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (Input.GetButton("Cancel")) GameEnd();

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press R to Restart";
                restart = true;
                break;
            }

        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if (score > nextExtend)
        {
            ChangeLife(1);
            nextExtend *= 2;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public int getLife()
    {
        return life;
    }

    public void ChangeLife(int lifeChange)
    {
        life += lifeChange;
        UpdateLife();
    }

    void UpdateLife()
    {
        lifeText.text = "Life: " + life;
    }

    public void ChangeEnergy(int e)
    {
        if (e > 0)
        {
            if (level < 4)
            {
                energy += e;
                if (energy >= 100)
                {
                    energy = 0;
                    ChangeLevel(1);
                }
            }
        }
        else
        {
            if (level >= 0)
            {
                energy += e;
                if (energy < 0)
                {
                    if (level > 0) energy = 99;
                    else energy = 0;
                    ChangeLevel(-1);
                }
            }
        }
        UpdateEnergy();
    }

    void UpdateEnergy()
    {
        if (level < 4)
            energyText.text = "Energy: " + energy + "/100";
        else
            energyText.text = "max";
    }

    public int getLevel()
    {
        return level;
    }

    public void ChangeLevel(int levelChange)
    {
        level += levelChange;
        if (level < 0) level = 0;
        UpdateLevel();
    }

    void UpdateLevel()
    {
        levelText.text = "Level: " + level;
    }

    public float getRank()
    {
        return rank;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}