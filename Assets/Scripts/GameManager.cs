using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text BestScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the brick map
        CreateBrickMap();

        ChangeBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            // Launch the ball
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            ChangeBestScore();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        //StageEnd();
    }

    void CreateBrickMap()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void ChangeBestScore()
    {
        // If new best score
        if (m_Points > MainManager.Instance.BestScore)
        {
            BestScoreText.text = $"Best Score : {MainManager.Instance.Name} : {m_Points}";
            MainManager.Instance.SaveData(m_Points);
            MainManager.Instance.LoadData();
        }
        else
        {
            if (MainManager.Instance.BestScore != 0) { BestScoreText.text = $"Best Score : {MainManager.Instance.BestName} : {MainManager.Instance.BestScore}"; }
        }
    }

    /*void StageEnd()
    {
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
        {
            Debug.Log("Stage end");
            // Pause the game + load new stage
        }
    }*/

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
