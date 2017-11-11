using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

	public Text distanceText;
	public GameObject trackedObject;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		distanceText.text = Vector3.Distance(trackedObject.transform.position, transform.position).ToString("0.0");
	}
}
