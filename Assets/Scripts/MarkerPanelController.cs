using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {
	public class MarkerPanelController : MonoBehaviour {

		public GameObject m_marker;
		public Button m_startButton;

		void OnEnable() {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			m_startButton.interactable = false;
			m_marker.SetActive(false);
		}

		void OnDisable() {
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
		}

		bool PlaceObjectOnHitPoint (GameObject obj, ARPoint point, ARHitTestResultType resultTypes)
		{
			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
			if (hitResults.Count > 0) {
				if (!m_marker.activeSelf) {
					m_marker.SetActive(true);
					m_startButton.interactable = true;
				}
				foreach (var hitResult in hitResults) {
					Debug.Log ("Got hit!");
					obj.transform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
					obj.transform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
					return true;
				}
			}
			return false;
		}

		void Update () {

			if (Input.touchCount > 0)
			{
				Debug.Log("Got touch!");
				// Check if finger is over a UI element
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
				{
					if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) { // no UI object was touched

						var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
						ARPoint point = new ARPoint {
							x = screenPosition.x,
							y = screenPosition.y
						};

						// prioritize reults types
						ARHitTestResultType[] resultTypes = {
							ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent,
							// if you want to use infinite planes use this:
							//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
							//ARHitTestResultType.ARHitTestResultTypeHorizontalPlane,
							ARHitTestResultType.ARHitTestResultTypeFeaturePoint
						};

						foreach (ARHitTestResultType resultType in resultTypes)
						{
							if (PlaceObjectOnHitPoint (m_marker, point, resultType))
							{
								return;
							}
						}
					}
				}
			}

		}
	}
}
