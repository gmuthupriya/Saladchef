using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPlateController : MonoBehaviour {

	public List<GameObject> Container;
	public int PLateID;
	// Use this for initialization
	void Start () {
		Container = new List<GameObject>(Constants.MAX_VEG_ON_PLATE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
