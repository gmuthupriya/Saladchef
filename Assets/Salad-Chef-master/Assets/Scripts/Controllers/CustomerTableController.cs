using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTableController : MonoBehaviour {

	public List<GameObject> Container;
	public List<string> RequestedCombination;
	public float WaitTime;
	public float StartTime;
	float burnrate = 1;
	bool delivered = false;

	public GameObject TimeLeft;
	public Transform[] spawnPoints;
	public int tableIndex;
	GameController gameController ;
	CustomerSpawnner customerSpawnner;

	float deafaultScale;
	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag(Constants.GAME_CONTROLLER_TAG).GetComponent<GameController>();
		customerSpawnner = GameObject.FindGameObjectWithTag(Constants.CUSTOMER_SPAWNER_TAG).GetComponent<CustomerSpawnner>();
		deafaultScale = TimeLeft.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		WaitForFood();
	}

	/// <summary>
	/// This function runs every frame and reduces Waiting time
	/// Incase of TIMEOUT both the players are penalised
	/// </summmary>
	void WaitForFood()
	{
		if(delivered == true){
			customerSpawnner.SpawnCustomerAt(tableIndex);
			Destroy(gameObject);
			return;
		}
		WaitTime -= (Time.deltaTime * burnrate);
		//Update the Progressbar
		float newScale = (WaitTime/StartTime) * deafaultScale;
		TimeLeft.transform.localScale = new Vector3(newScale,TimeLeft.transform.localScale.y,TimeLeft.transform.localScale.y);
		if(WaitTime <= 0){
			GameObject[] Players =  GameObject.FindGameObjectsWithTag(Constants.PLAYER_TAG);
			foreach (var player in Players)
			{
				player.GetComponent<PlayerController>().PlayerScore += Constants.PENALTY_POINTS;
			}
			customerSpawnner.SpawnCustomerAt(tableIndex);
			Destroy(gameObject);
		}
	}


	/// <summary>
	/// This function is used to calculate FEEDBACK SCORE incase of 
	/// Wrong Delivery A Negative score is returned .This function is called by players hence its public
	/// </summmary>
	public int FeedBack(int playerID){
		int count = RequestedCombination.Count;
		int match = 0 ;
		foreach (var item in Container)
		{
			if(RequestedCombination.Contains(item.name)){
				match++;
			}
		}
		float completionRate = (WaitTime / StartTime ) * 100;
		if(count == match  && completionRate > Constants.GOOD_PERCENT){
			delivered = true;
			//Spawn Pickups
			gameController.SpawnPowerUp(playerID);
			return Constants.REWARD_POINTS;
		}else if(count == match){
			delivered = true;
			return Constants.REWARD_POINTS;
		}else{
			burnrate = 2;
			return Constants.PENALTY_POINTS;
		}
	}



	
}
