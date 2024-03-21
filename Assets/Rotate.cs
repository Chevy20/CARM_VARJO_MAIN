using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
public class Rotate : MonoBehaviour
{
    private Hand LeftHand;
    private Hand RightHand;
    private float leftGrabStrength;
    private float rightGrabStrength;
    private bool isLeftGrabbing;
    private bool isRightGrabbing;
    
    public float rotationSpeed = 20f;  // Adjust this value to change the rotation speed
    private Vector3 previousRightHandPosition = Vector3.zero;
    private Vector3 previousLeftHandPosition = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LeftHand = Hands.Provider.GetHand(Chirality.Left);
        RightHand = Hands.Provider.GetHand(Chirality.Right);
        if (LeftHand != null)
        {
            leftGrabStrength = LeftHand.GrabStrength;
            isLeftGrabbing = false;
            previousLeftHandPosition = LeftHand.PalmPosition;
            if (leftGrabStrength > 0.90 && !isLeftGrabbing) //We check isLeftGrabbing so this code isn't called if we are already grabbing.
            {
                isLeftGrabbing = true; // grab strength is over 0.8 threshold so it is true
                Debug.Log("Left Grab");
            }
            else if (leftGrabStrength < 0.2 && isLeftGrabbing) //We check isLeftGrabbing so this code isn't called if we aren't already grabbing.
            {
                isLeftGrabbing = false; // grab strength is less than 0.7 so no chance of jittering grab.
            }
        }

        if (RightHand != null)
        {
            rightGrabStrength = RightHand.GrabStrength;
            isRightGrabbing = false;
            // Update previousRightHandPosition with the current hand position
            previousRightHandPosition = RightHand.PalmPosition;


            if (rightGrabStrength > 0.90 && !isRightGrabbing) //We check isRightGrabbing so this code isn't called if we are already grabbing.
            {
                isRightGrabbing = true; // grab strength is over 0.8 threshold so it is true
                //Debug.Log("Right Grab");
            }
            else if (rightGrabStrength < 0.2 && isRightGrabbing) //We check isRightGrabbing so this code isn't called if we aren't already grabbing.
            {
                isRightGrabbing = false; // grab strength is less than 0.7 so no chance of jittering grab.
            }
        }


    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "RightHandCollider" && isRightGrabbing)
        {
            // Get the carm GameObject
            GameObject carm = this.transform.parent.parent.gameObject;
            // Get the direction of the hand movement
            Vector3 handMovementDirection = RightHand.PalmPosition - previousRightHandPosition;

            // Calculate the angle to rotate based on the hand movement direction
            // Calculate the angle to rotate based on the hand movement direction
            float angle = Vector3.SignedAngle(carm.transform.forward, handMovementDirection, carm.transform.up);

            // Invert the angle
            angle *= -1;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.AngleAxis(angle, carm.transform.forward);

            // Increase the rotation speed
            

            // Gradually rotate towards the target rotation at the specified speed
            carm.transform.rotation = Quaternion.RotateTowards(carm.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Rest of your code...
            // Get the current rotation angles
            Vector3 currentRotation = carm.transform.eulerAngles;

            // Convert to -180 to 180 degree format if necessary
            if (currentRotation.z > 180) currentRotation.z -= 360;

            // Clamp the z rotation
            currentRotation.z = Mathf.Clamp(currentRotation.z, -53f, 53f);

            // Apply the clamped rotation
            carm.transform.eulerAngles = currentRotation;

            // Update the previous hand position
            previousRightHandPosition = RightHand.PalmPosition;
        }
        else if (other.gameObject.name == "LeftHandCollider" && isLeftGrabbing)
        {
            // Get the carm GameObject
            GameObject carm = this.transform.parent.parent.gameObject;
            // Get the direction of the hand movement
            Vector3 handMovementDirection = LeftHand.PalmPosition - previousLeftHandPosition;

            // Calculate the angle to rotate based on the hand movement direction
            // Calculate the angle to rotate based on the hand movement direction
            float angle = Vector3.SignedAngle(carm.transform.forward, handMovementDirection, carm.transform.up);

            // Invert the angle
            angle *= -1;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.AngleAxis(angle, carm.transform.forward);

            // Increase the rotation speed
            

            // Gradually rotate towards the target rotation at the specified speed
            carm.transform.rotation = Quaternion.RotateTowards(carm.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Rest of your code...
            // Get the current rotation angles
            Vector3 currentRotation = carm.transform.eulerAngles;

            // Convert to -180 to 180 degree format if necessary
            if (currentRotation.z > 180) currentRotation.z -= 360;

            // Clamp the z rotation
            currentRotation.z = Mathf.Clamp(currentRotation.z, -53f, 53f);

            // Apply the clamped rotation
            carm.transform.eulerAngles = currentRotation;

            // Update the previous hand position
            previousRightHandPosition = LeftHand.PalmPosition;
        }

    }
}
