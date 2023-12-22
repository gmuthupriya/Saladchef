using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public KeyCode LeftKey  = KeyCode.A;
	public KeyCode RightKey = KeyCode.D;
	public KeyCode UpKey = KeyCode.W;
	public KeyCode DownKey = KeyCode.S;
	public KeyCode PickKey = KeyCode.E;
	public KeyCode DropKey = KeyCode.R;

	public int PlayerID;
	public float PlayerSpeed = 2.0f;
	public float TimeLeft = Constants.DEFAULT_TIME_LEFT;
	public List<GameObject> Basket;
	public List<GameObject> Container;

	public int PlayerScore;

	private bool canMove = true;
	private float defaultSpeed ;
	// Use this for initialization
	void Start () {
		//Player can carry two objects at max
		defaultSpeed = PlayerSpeed;
		Basket = new List<GameObject>(Constants.BASKET_SIZE);
		Container = new List<GameObject>(Constants.MAX_VEGTABLES_ON_BOARD);
	}
	
	// Update is called once per frame
	void Update () {
		CheckForTimeOut();
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// All the Physics based Functions are called from this function
	/// </summary>
	void FixedUpdate()
	{
		MovePlayer();
	}
	

	
	/// <summary>
	/// This function is used for the movement of the player accoriding to input.
	/// </summary>
	void MovePlayer()
	{
		if(!canMove){
			return;
		}
		transform.rotation = Quaternion.Euler(0,0,0);
		//Move Player depending on the key pressed
		if(Input.GetKey(UpKey)){
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,PlayerSpeed);
		}		
		else if(Input.GetKey(DownKey)){
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-PlayerSpeed);
		}
		else if(Input.GetKey(LeftKey)){
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-PlayerSpeed,0);
		}
		else if(Input.GetKey(RightKey)){
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerSpeed,0);
		}else{
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		}
	}


	/// <summary>
	///This function is used to pick a vegetable from a vegetable rack
	///if basket is already full then nothnig can be picked
	/// </summary>
	///<param name="other">The other Collider2D involved in this collision.</param>
	void Pick(Collider2D other)
	{
		if(Basket.Count == Constants.BASKET_SIZE){
			//if full return 
			return;
		}
		if(other.gameObject.tag.Equals(Constants.VEGETABLE_TAG)){
			GameObject vegClone = Instantiate(other.gameObject,transform.position,Quaternion.identity);
			Basket.Add(vegClone);
			vegClone.transform.parent = transform;
			vegClone.tag = "Untagged";
			Vector3 currentPose = vegClone.transform.position ;
			currentPose.x += Basket.Count ; 
			vegClone.transform.position = currentPose;
		}
		if(other.gameObject.tag.Equals(Constants.CHOPBOARD_TAG)){
			ChopBoardController chopcontroller = other.gameObject.GetComponent<ChopBoardController>();
			//if not players chopboard  or if basket is not empty then return 
			if(chopcontroller.ChopBoardID != PlayerID && Basket.Count != 0){
				return;
			}
			//collect combination from chopbord
			foreach (var item in chopcontroller.Container)
			{

				Container.Add(item);
				item.transform.parent = gameObject.transform;
			}
				chopcontroller.Container.Clear();

			
		}
		if(other.gameObject.tag.Equals(Constants.EXTRA_PLATE_TAG)){
			ExtraPlateController extraPlate = other.gameObject.GetComponent<ExtraPlateController>();
			//return if not my plate or plate is empty or basket is full
			if(PlayerID != extraPlate.PLateID || extraPlate.Container.Count == 0 || Basket.Count == Constants.BASKET_SIZE){
				return;
			}
			//Or elese remove from plate and add to my basket
			GameObject vegetable = extraPlate.Container[extraPlate.Container.Count-1];
			extraPlate.Container.Remove(vegetable);
			Basket.Add(vegetable);
			vegetable.transform.parent = transform;
		}

	}

	/// <summary>
	///This functiion is used to drop a vegetable from basket
	///Which ever object was picked last will be dropped
	/// </summary>
	///<param name="other">The other Collider2D involved in this collision.</param>
	void Drop(Collider2D other)
	{
		if(Basket.Count == 0 && Container.Count == 0){
			//Nothing to drop return
			return;
		};
		//else check the colliding object
		if(other.gameObject.tag.Equals(Constants.CHOPBOARD_TAG)){
			//remove from basket and add to chopboard
			ChopBoardController chopcontroller = other.gameObject.GetComponent<ChopBoardController>();
			if(chopcontroller.ChopBoardID != PlayerID){
				return;
			}
			if(chopcontroller.Container.Count < chopcontroller.Container.Capacity){
				if(Basket.Count == 0){
					return;
				}
				GameObject veg  = Basket[Basket.Count-1];
				chopcontroller.Container.Add(veg);
				veg.transform.parent = chopcontroller.gameObject.transform;
				Basket.Remove(veg);
				//disable movemnt for 2 secs
				StartCoroutine(DisableMovement(Constants.CHOPTIME));
			}
		}else if(other.gameObject.tag.Equals(Constants.TRASH_TAG)){
				foreach (var item in Container)
				{
					Destroy(item);
				}
				Container.Clear();
				//Penalty points
				PlayerScore += Constants.PENALTY_POINTS;
		}else if(other.gameObject.tag.Equals(Constants.EXTRA_PLATE_TAG)){
			ExtraPlateController extraPlate =  other.gameObject.GetComponent<ExtraPlateController>();
			if(extraPlate.PLateID != PlayerID || extraPlate.Container.Count == Constants.MAX_VEG_ON_PLATE){
				//return cant drop on other players plate or if plate is full then also return
				return;
			}
			//drop the last element on plate
			GameObject veg  = Basket[Basket.Count-1];
			Basket.Remove(veg);
			extraPlate.Container.Add(veg);
			veg.transform.parent = extraPlate.transform;
		}else if(other.gameObject.tag.Equals(Constants.CUSTOMER_TABLE)){
			//if my container is emplty return must pick up combination first 
			if(Container.Count == 0){
				return;
			}
			//Drop on customer table
			CustomerTableController customerTableController = other.gameObject.GetComponent<CustomerTableController>();
			foreach (var item in Container)
			{
				customerTableController.Container.Add(item);
				item.transform.parent = customerTableController.gameObject.transform;
			}
			Container.Clear();
			//get feedback for delivery

			int Feedbackscore = customerTableController.FeedBack(PlayerID);
			PlayerScore += Feedbackscore;
			//Debug.Log("player no " + PlayerID + " got "+ Feedbackscore);
		}

	}

	/// <summary>
	///This function is used deceide pick or drop depending on the input of the user
	/// </summary>
	///<param name="other">The other Collider2D involved in this collision.</param>
	void PickOrDrop(Collider2D other){
		if(Input.GetKeyDown(PickKey)){
			Pick(other);
		}else if(Input.GetKeyDown(DropKey)){
			Drop(other);
		}
	}

	/// <summary>
	/// Sent each frame where another object is within a trigger collider
	/// attached to this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerStay2D(Collider2D other)
	{
		PickOrDrop(other);
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals(Constants.POWERUP_TAG)){
			PowerUp powerup = other.gameObject.GetComponent<PowerUp>();
			if(powerup.PalyerID == PlayerID){
				ConsumePowerUp( powerup);
				//Delete powerup
				Destroy(other.gameObject);
			}
		}	
	}

	IEnumerator DisableMovement(float secs)
	{
		canMove = false;
		yield return new WaitForSeconds(secs);
		canMove = true;
	}

	/// <summary>
	/// This function is used to consume a powerup.
	/// </summary>
	/// <param name="powerUp">The Powerup Component of the  powerup script.</param>
	void ConsumePowerUp(PowerUp powerUp)
	{
		switch (powerUp.power)
		{
			case Constants.PowerUp.SpeedBooster:
				StartCoroutine(EnhanceSpeed());
				break;

			case Constants.PowerUp.TimeBooster:
				TimeLeft += Constants.TIME_BONUS;
				break;

			case Constants.PowerUp.ExtraScore:
				PlayerScore += Constants.SCORE_BONUS;
			break;

			default:
			break;
		}
	}

	IEnumerator EnhanceSpeed()
	{
		PlayerSpeed = PlayerSpeed * Constants.SPPED_BOOST_FACTOR;
		yield return new WaitForSeconds(Constants.POWEUP_DURATION);
		PlayerSpeed = defaultSpeed;
	}

	void CheckForTimeOut()
	{
		TimeLeft -= Time.deltaTime;
		if(TimeLeft <= 0){
			canMove = false;
            TimeLeft = 0;
        }
	}

}
