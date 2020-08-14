using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameState : MonoBehaviour
{

    public GameObject obstacle;
    public GameObject ground;
    public GameObject player;
    public TextMeshProUGUI gameOverText;
    public GameObject tryAgainButton;
    private int levelUpDistance = 200;
    private float distanceBetweenBlocks = 5f;
    private float zPosition = 15f;
    private float groundWidth;
    private float blockWidth;
    private System.Random rand = new System.Random();
    private float blockLevel;
    private int distanceMetric = 0;

    void Start()
    {
        groundWidth = (int)ground.transform.localScale.x * 10;
        blockWidth = (int)obstacle.transform.localScale.x;
        blockLevel = blockLevel = (int)groundWidth / 30;
        InvokeRepeating("clearUnusedObjects", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        PopulateFieldOfObstacles();
    }

    private void PopulateFieldOfObstacles()
    {
        if (zPosition - player.transform.position.z <= 100)
        {
            int newDistanceMetric = (int)player.transform.position.z / levelUpDistance;

            if (newDistanceMetric > distanceMetric)
            {
                blockLevel += .5f;
                //TODO level up reward
                distanceMetric = newDistanceMetric;
            }

            zPosition = zPosition + distanceBetweenBlocks;
            var xVariance = (groundWidth / 2) - (blockWidth / 2);
            var numPositions = groundWidth - (blockWidth - 1);
            var xPositionsInRow = new List<float>();

            for (var i = 0; i < numPositions; i++)
            {
                xPositionsInRow.Add(-xVariance + i);
            }

            //introcude some variance in num blocks for slower buildups
            var numBlocks = blockLevel;
            if (blockLevel % 1 == .5f)
            {
                numBlocks = numBlocks + (float)(rand.Next(0, 2) * 2 - 1) * .5f;
            }

            for (var i = 0; i < numBlocks; i++)
            {
                // make sure obstacles don't overlap
                var xIndex = rand.Next(0, xPositionsInRow.Count);
                var xPos = xPositionsInRow[xIndex];
                xPositionsInRow.RemoveAt(xIndex);
                var newPosition = new Vector3(xPos, .5f, zPosition);
                Instantiate(obstacle, newPosition, Quaternion.identity);
            }
        }
    }

    public void endGame()
    {
        Invoke("gameOver", 1f);
    }

    private void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        tryAgainButton.SetActive(true);
    }

    public void restore()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void clearUnusedObjects()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        var unusedObstacles = obstacles.Where(o => o.transform.position.z < player.transform.position.z - 5);
        foreach (GameObject obstacle in unusedObstacles)
        {
            Destroy(obstacle);
        }
    }

}
