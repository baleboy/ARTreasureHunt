using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {
	public class GameLogic : MonoBehaviour {

		public DetectorController m_detector;
		public GameObject m_marker;
		public GameObject m_camera;
		public Text m_distanceText;
		public Text m_debugText;

		// Use this for initialization
		void Start () {
			m_detector.SetScale(0.2f,5);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		void OnEnable()
    {
        UnityARSessionNativeInterface.ARSessionTrackingChangedEvent += TrackingChanged;
    }

		void TrackingChanged(UnityARCamera camera) {
			Debug.Log("Tracking changed");

			if (camera.trackingState == ARTrackingState.ARTrackingStateNormal) {
				m_debugText.text = "State: Normal (" + camera.trackingState.ToString() + ")";
			}
			if (camera.trackingState == ARTrackingState.ARTrackingStateLimited) {
				m_debugText.text = "State: Limited (" + camera.trackingState.ToString() + ")";
			}
			// m_debugText.text = camera.trackingState.ToString() + ", " + camera.trackingReason.ToString();
		}
		// Update is called once per frame
		void Update () {
			float distance = Vector3.Distance(m_marker.transform.position, m_camera.transform.position);
			m_detector.SetValue(distance);
			m_distanceText.text = distance.ToString("N");
		}
	}
}
