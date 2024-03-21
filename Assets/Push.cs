using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
public class Push : MonoBehaviour
{
    private Hand LeftHand;
    private Hand RightHand;
    private float leftGrabStrength;
    private float rightGrabStrength;
    private bool isLeftGrabbing;
    private bool isRightGrabbing;
    private bool isCollidingWithBackLimit = false;
    private bool isCollidingWithFrontLimit = false;
    public GameObject carm;


    private Vector3 previousRightHandPosition = Vector3.zero;
    private Vector3 previousLeftHandPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

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
            // Calculate the difference in the Z position of the hand between the current and the previous frame
            float handMovementZ = RightHand.PalmPosition.z - previousRightHandPosition.z;
            Debug.Log(handMovementZ);
            // Get the current position of the carm GameObject
            Vector3 carmPosition = carm.transform.position;

            // Add the hand movement to the Z position of the carm GameObject
            // But only if the carm GameObject is not colliding with the BackLimitCollider or the hand is moving in the +Z direction
            if ((!isCollidingWithBackLimit || handMovementZ < 0) && (!isCollidingWithFrontLimit || handMovementZ > 0))
            {
                carmPosition.z += handMovementZ;
            }


            // Update the position of the carm GameObject
            carm.transform.position = carmPosition;

            // Update the previous hand position
            previousRightHandPosition = RightHand.PalmPosition;

        }
         else if (other.gameObject.name == "LeftHandCollider" && isLeftGrabbing)
        {
            // Calculate the difference in the Z position of the hand between the current and the previous frame
            float handMovementZ = LeftHand.PalmPosition.z - previousLeftHandPosition.z;
            Debug.Log(handMovementZ);
            // Get the current position of the carm GameObject
            Vector3 carmPosition = carm.transform.position;

            // Add the hand movement to the Z position of the carm GameObject
            // But only if the carm GameObject is not colliding with the BackLimitCollider or the hand is moving in the +Z direction
            if ((!isCollidingWithBackLimit || handMovementZ < 0) && (!isCollidingWithFrontLimit || handMovementZ > 0))
            {
                carmPosition.z += handMovementZ;
            }


            // Update the position of the carm GameObject
            carm.transform.position = carmPosition;

            // Update the previous hand position
            previousLeftHandPosition = LeftHand.PalmPosition;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BackLimitCollider")
        {
            isCollidingWithBackLimit = true;
            Debug.Log("Colliding with back limit");
        }
        if (other.gameObject.name == "FrontLimitCollider")
        {
            isCollidingWithFrontLimit = true;
            Debug.Log("Colliding with front limit");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "BackLimitCollider")
        {
            isCollidingWithBackLimit = false;
        }
        if (other.gameObject.name == "FrontLimitCollider")
        {
            isCollidingWithFrontLimit = false;
            Debug.Log("Colliding with front limit");
        }
    }
}
