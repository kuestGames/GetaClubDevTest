using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FollowPoints : MonoBehaviour {

    //Curve creation variables
    public List<Transform> wayPoints = new List<Transform> { };
    public bool isLoop = true;
    [Range(0.005f, 0.5f)]
    public float curveStepSize = 0.02f;

    //Curve Follow variables
    public Transform follower;
    public float followSpeed = 1f;
    public FollowRotationType rotationType;
    public Transform objectToFocus;
    public bool isMoving = true;
    private int currentTargetIndex = 0;
    private int nextTargetIndex = 1;
    private float curveProgress; //normalized progress of the current curve

    public enum FollowRotationType {
        RotateWithMovement,
        FocusOnObject,
        NoRotation
    }

    void Start() {

    }

    void FixedUpdate() { // depending on requirements could be moved to regular Update for a smoother movemente
        if (isMoving) {
            curveProgress += Time.fixedDeltaTime * followSpeed;
            if (curveProgress >= 1) {
                curveProgress = 1 - curveProgress;
                currentTargetIndex = nextTargetIndex;
                if (nextTargetIndex >= (wayPoints.Count - 1)) { //End of the shape
                    if (isLoop) { //end of the shape, next target is starting point
                        nextTargetIndex = 0;
                    }
                    else { //End of the shape and end movement
                        isMoving = false;
                        follower.localPosition = wayPoints[nextTargetIndex].localPosition;
                        return;

                    }
                }
                else {
                    nextTargetIndex++;
                }
            }
            else {
                //Position Calculation
                Vector3 tempPosition = Mathf.Pow(1 - curveProgress, 3) * wayPoints[currentTargetIndex].localPosition + 3 * Mathf.Pow(1 - curveProgress, 2) * curveProgress * wayPoints[currentTargetIndex].Find("ControlNode2").position + 3 * (1 - curveProgress) * Mathf.Pow(curveProgress, 2) * wayPoints[nextTargetIndex].Find("ControlNode").position + Mathf.Pow(curveProgress, 3) * wayPoints[nextTargetIndex].localPosition;
                follower.position = tempPosition;
            }

            switch (rotationType) {
                case FollowRotationType.RotateWithMovement:
                    break;
                case FollowRotationType.FocusOnObject:
                    follower.LookAt(objectToFocus);
                    break;
                case FollowRotationType.NoRotation:
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDrawGizmos() {
        Vector3 temp_segmentGizmo = Vector3.zero;
        Gizmos.DrawLine(wayPoints[0].localPosition, wayPoints[0].Find("ControlNode").position);
        Gizmos.DrawLine(wayPoints[0].localPosition, wayPoints[0].Find("ControlNode2").position);
        for (int j = 1; j < wayPoints.Count; j++) {
            for (float i = 0; i <= 1; i += curveStepSize) { //puntos 
                temp_segmentGizmo = Mathf.Pow(1 - i, 3) * wayPoints[j-1].localPosition + 3 * Mathf.Pow(1 - i, 2) * i * wayPoints[j-1].Find("ControlNode2").position + 3 * (1 - i) * Mathf.Pow(i, 2) * wayPoints[j].Find("ControlNode").position + Mathf.Pow(i, 3) * wayPoints[j].localPosition;
                Gizmos.DrawCube(temp_segmentGizmo, new Vector3(0.15f, 0.15f, 0.15f));
            }
            Gizmos.DrawLine(wayPoints[j].localPosition, wayPoints[j].Find("ControlNode").position);
            Gizmos.DrawLine(wayPoints[j].localPosition, wayPoints[j].Find("ControlNode2").position);
        }

        if (isLoop) {
            for (float i = 0; i <= 1; i += curveStepSize) { //puntos 
                temp_segmentGizmo = Mathf.Pow(1 - i, 3) * wayPoints[wayPoints.Count-1].localPosition + 3 * Mathf.Pow(1 - i, 2) * i * wayPoints[wayPoints.Count-1].Find("ControlNode2").position + 3 * (1 - i) * Mathf.Pow(i, 2) * wayPoints[0].Find("ControlNode").position + Mathf.Pow(i, 3) * wayPoints[0].localPosition;
                Gizmos.DrawCube(temp_segmentGizmo, new Vector3(0.15f, 0.15f, 0.15f));
            }
        }
    }
}
