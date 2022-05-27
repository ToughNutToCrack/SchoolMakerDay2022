using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARRaycastManager))]
public class GameLogiController : MonoBehaviour {
    public GameObject objectToSpawn;

    ARRaycastManager raycastManager;
    GameObject spawnedObject;


    void Awake() {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update() {
        if (spawnedObject == null) {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    var touchPos = touch.position;

                    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

                    if (raycastManager.Raycast(touchPos, hitResults, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)) {
                        var hitPose = hitResults[0].pose;
                        spawnedObject = Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
                    }
                }
            }
        }
    }
}
