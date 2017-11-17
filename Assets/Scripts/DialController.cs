using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialController : MonoBehaviour {

	float min_rotation = -80;
	float max_rotation = 80;
	float m_rotation = -80;

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame

	public void SetIntensity(float value) {
		m_rotation = min_rotation + value * (max_rotation - min_rotation);
	}

	void Update () {
    	transform.eulerAngles = new Vector3(0, 0, m_rotation);
	}
}
