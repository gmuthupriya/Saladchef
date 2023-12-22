using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopBoardController : MonoBehaviour {
	public List<GameObject> Container;
	public int ChopBoardID;
	// Use this for initialization
	void Start () {
		Container = new List<GameObject>(Constants.MAX_VEGTABLES_ON_BOARD);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
