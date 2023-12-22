using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public delegate void GameEvent();
    public static event GameEvent OnGameOver;
    GameObject[] PickupPoints;
    public GameObject[] PowerUps;
    public GameObject[] Players;
    public GameObject WinningMessage;
    public Text WiningText;

    // Use this for initialization
    void Start()
    {
        PickupPoints = GameObject.FindGameObjectsWithTag(Constants.PICKUP_SPAWN_POINT_TAGS);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayerTimes();
    }

    /// <summary>
    /// This function is used to spawn powerups
    /// </summmary>
    ///<param name="forPlayer">The Player ID who is eligible to pick this poerup.</param>
    public void SpawnPowerUp(int forPlayer)
    {
        Vector3 RandomSpawnPoint = PickupPoints[Random.Range(0, PickupPoints.Length)].transform.position;
        int PoerupIndex = Random.Range(0, PowerUps.Length);
        GameObject powerUp = Instantiate(PowerUps[PoerupIndex], RandomSpawnPoint, Quaternion.identity);
        powerUp.GetComponent<PowerUp>().PalyerID = forPlayer;
        //Destroy the powerup after 6 seconds
        Destroy(powerUp, 6);
    }


    void LookAtPlayerTimes()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerController pc = Players[i].GetComponent<PlayerController>();
            if (pc.TimeLeft > 0)
            {
                return;
            }
        }
        //if code reaches here only if none of the players have positive times
        int P1Score = Players[0].GetComponent<PlayerController>().PlayerScore;
        int P2Score = Players[1].GetComponent<PlayerController>().PlayerScore;

        WinningMessage.SetActive(true);
        if (P1Score > P2Score)
        {
            WiningText.text = "Player 1 Won";
        }
        else if (P2Score > P1Score)
        {
            WiningText.text = "Player 2 Won";
        }
        else
        {
            WiningText.text = "Match Draw";
        }

        if (OnGameOver != null)
        {
            OnGameOver();
        }


    }

    
}
