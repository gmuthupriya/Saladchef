using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnner : MonoBehaviour {
	public GameObject Customer;
	public Transform[] CustomerPoints;

	public string[] VegNames;
	public GameObject[] VegSprites;
	// Use this for initialization
	void Start () {
		SpawnAllCustomer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnAllCustomer()
	{
		for(int i = 0; i< CustomerPoints.Length ; i++){
			SpawnCustomerAt(i);
		}
	}

	public void SpawnCustomerAt(int tableIndex){
		GameObject customer = Instantiate(Customer,CustomerPoints[tableIndex].position,Quaternion.identity);
		CustomerTableController customerTable =  customer.GetComponent<CustomerTableController>();
		int CombinationOfVegs = Random.Range(1,Constants.MAX_VEGTABLES_ON_BOARD+1);
		for(int j = 0 ; j < CombinationOfVegs ; j++){
			//do this until rquired combination is formed
			int randomVeg = 0;
			do{
				 randomVeg = Random.Range(0,VegNames.Length);
			}
			while(customerTable.RequestedCombination.Contains(VegNames[randomVeg]));
			customerTable.RequestedCombination.Add(VegNames[randomVeg]+"(Clone)");
			GameObject vegetableSprite = Instantiate(VegSprites[randomVeg],Vector3.zero,Quaternion.identity);
			vegetableSprite.transform.parent = customer.transform;
			vegetableSprite.transform.position = customerTable.spawnPoints[j].transform.position; 
		}
		customerTable.WaitTime = Constants.WAIT_TIME_PER_VEG * CombinationOfVegs;
		customerTable.StartTime = Constants.WAIT_TIME_PER_VEG * CombinationOfVegs;
		customerTable.tableIndex = tableIndex;
	}

	


}
