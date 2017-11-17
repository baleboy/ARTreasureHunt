using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {

	public class ScanningPanelController : MonoBehaviour {

		public GameObject m_ScanningPanel;
		public GameObject m_MarkerPanel;
		public Text m_debugText;

		private ARTrackingState m_tracking_state = ARTrackingState.ARTrackingStateNotAvailable;

		// Use this for initialization
		void Start () {
        UnityARSessionNativeInterface.ARSessionTrackingChangedEvent += TrackingChanged;
		}

		void TrackingChanged(UnityARCamera camera) {
			Debug.Log("Tracking changed");
			m_tracking_state = camera.trackingState;

			if (camera.trackingState == ARTrackingState.ARTrackingStateNormal) {
				m_debugText.text = "State: Normal (" + camera.trackingState.ToString() + ")";
				m_ScanningPanel.SetActive(false);
				m_MarkerPanel.SetActive(true);
			}
			if (camera.trackingState == ARTrackingState.ARTrackingStateLimited) {
				m_debugText.text = "State: Limited (" + camera.trackingState.ToString() + ")";
			}
		}

		// Update is called once per frame
		void Update () {

		}
	}
}
