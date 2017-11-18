using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {

	public class ScanningPanelController : MonoBehaviour {

		public GameObject m_ScanningPanel;
		public GameObject m_MarkerPanel;
		public Text m_debugText;
		const int MIN_FEATURE_POINTS = 20;

		void OnEnable() {
			Debug.Log("Scanning Panel enabled");
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdate;
		}

		void OnDisable() {
			Debug.Log("Scanning Panel disabled");
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
			UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdate;
		}

		void ARFrameUpdate(UnityARCamera camera){
			Debug.Log("Number of feature points: " + camera.pointCloudData.Length);
			if (camera.pointCloudData.Length >= MIN_FEATURE_POINTS) {
				m_debugText.text = "Got enough feature points";
				m_ScanningPanel.SetActive(false);
				m_MarkerPanel.SetActive(true);
			}
		}
	}
}
