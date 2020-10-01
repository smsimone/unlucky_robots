using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject terrain;
    public List<GameObject> electricTilePrefabs;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI PlayerWonText;
    public Button restartButton;
    public Button menuButton;
    public bool isGameActive = true;
    private float spawnRate = 1.0f;
    public int timeLeft = 32;

    private List<GameObject> spawned;

    public float tileSpawnTime = 4f;

    public PlayerController player1;
    public PlayerController player2;

    public CreateRobot robot1;
    public CreateRobot robot2;
    private SoundManager soundManager;


    void Start()
    {
        spawned = new List<GameObject>();
        if (electricTilePrefabs != null && electricTilePrefabs.Count > 0)
        {
            StartCoroutine(SpawnTileRoutine());
        }

        StartCoroutine(Timer());
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (timeLeft == 30)
        {
            soundManager.LastSeconds();
        }
        else if (timeLeft == 0)
        {
            soundManager.End();
        }

        if (robot1.HasFinished() && !robot2.HasFinished())
        {
            PlayerWon(1);
        }
        else if (!robot1.HasFinished() && robot2.HasFinished())
        {
            PlayerWon(2);
        }
    }

    private void SpawnNewTile()
    {
        Transform t = terrain.GetComponent<Transform>();
        Vector3 scale = t.localScale;

        float xVal = Mathf.Floor(Random.Range(0, scale.x));
        float zVal = Mathf.Floor(Random.Range(0, scale.z));

        //todo evitare di spawnare gli oggetti uno sopra l'altro
        Vector3 newScale = new Vector3(xVal, t.localPosition.y + 0.1f, zVal);

        GameObject tile = electricTilePrefabs[Random.Range(0, electricTilePrefabs.Count)];

        GameObject testObj = Instantiate(tile, newScale, Quaternion.identity);
        //testObj.GetComponent<Renderer>().material.color = Color.red;

        spawned.Add(testObj);
    }


    private IEnumerator SpawnTileRoutine()
    {
        for (; ; )
        {
            SpawnNewTile();
            yield return new WaitForSeconds(tileSpawnTime);
        }
    }

    IEnumerator Timer()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            timerText.text = timeLeft / 60 + "m:" + timeLeft % 60 + "s";
            timeLeft--;
            if (timeLeft % 10 == 0)
            {
                if (powerUpPrefab != null)
                {
                    Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
                }
            }

            if (timeLeft < 0)
                GameOver();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        if(!robot2.HasFinished() && !robot1.HasFinished())
            PlayerWonText.text = "Draw";

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("OverRobot");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    private Vector3 GenerateSpawnPosition()
    {

        float spawnPosX = Random.Range(-8, 9);
        while (spawnPosX > -1 && spawnPosX < 2.2f)
            spawnPosX = Random.Range(-8, 9);
        float spawnPosZ = Random.Range(-9, 2);

        return new Vector3(spawnPosX, 1, spawnPosZ);
    }

    private void PlayerWon(int num)
    {
        timeLeft = 0;
        PlayerWonText.text = "Player " + num + " won!!";

    }

}