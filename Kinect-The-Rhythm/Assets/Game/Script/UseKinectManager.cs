using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseKinectManager : MonoBehaviour
{
    private KinectManager kinectManager;
    public RawImage kinectImg;

    // Use this for initialization
    void Start()
    {
        kinectManager = KinectManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //kinectImg.transform.SetSiblingIndex(99);
        /*
        if (kinectManager && kinectManager.IsInitialized())
        {
            if (kinectImg != null&& kinectImg.texture == null)
            {
                //   Texture2D usersClrTex = kinectManager.GetUsersClrTex();  
                Texture2D usersLblTex = kinectManager.GetUsersLblTex();
                kinectImg.texture = usersLblTex;
            }
            if (kinectManager.IsUserDetected())
            {
                long userId = kinectManager.GetPrimaryUserID();
                Vector3 userPosition = kinectManager.GetUserPosition(userId);

                int rightHandJoint = (int)KinectInterop.JointType.HandRight;
                if (kinectManager.IsJointTracked(userId, rightHandJoint))
                {
                    Vector3 rightHandPosition = kinectManager.GetJointKinectPosition(userId, rightHandJoint);
                    // print("X=" + rightHandPosition.x + ",Y=" + rightHandPosition.y + ",Z=" + rightHandPosition.z);
                }
            }
        }
        */
    }
}
