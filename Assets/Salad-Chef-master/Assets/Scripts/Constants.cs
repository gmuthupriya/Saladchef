using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants  {

	
	public const string VEGETABLE_TAG = "Vegetable";
	public const string TRASH_TAG  = "Trash";
	public const string CHOPBOARD_TAG = "ChopBoard";
	public const string EXTRA_PLATE_TAG = "ExtraPlate";
	public const string CUSTOMER_TABLE = "CustomerTable";
	public const string PICKUP_SPAWN_POINT_TAGS = "SpawnPoints";
	public const string CUSTOMER_SPAWNER_TAG = "CustomerSpawner";
	public const string PLAYER_TAG = "Player";
	public const string POWERUP_TAG = "PowerUp";
	public const string GAME_CONTROLLER_TAG = "GameController";
	public const float DEFAULT_TIME_LEFT = 120f;
	public const float POWEUP_DURATION = 8;
	public const int BASKET_SIZE  = 2;
	public const int MAX_VEGTABLES_ON_BOARD = 3;

	public const float WAIT_TIME_PER_VEG = 60;
	public const int GOOD_PERCENT = 70;
	public const int MAX_VEG_ON_PLATE = 1;
	public const int PENALTY_POINTS = -20;
	public const int REWARD_POINTS = 40;

	public const int TIME_BONUS = 30;
	public const int SPPED_BOOST_FACTOR = 2;
	public const float CHOPTIME = 4;
	public const int SCORE_BONUS = 40;

	
	public enum PowerUp {
		TimeBooster = 0,
		ExtraScore = 1,
		SpeedBooster = 2,
	}
}
