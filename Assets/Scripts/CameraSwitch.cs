using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager ARCamManager;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Debug.Log("KSH Touch : " + ARCamManager.currentFacingDirection);

                if (ARCamManager.currentFacingDirection == CameraFacingDirection.World)
                {
                    ARCamManager.requestedFacingDirection = CameraFacingDirection.User;
                    Debug.Log("KSH Touch 1 : " + ARCamManager.currentFacingDirection);
                }
                else if (ARCamManager.currentFacingDirection == CameraFacingDirection.User)
                {
                    ARCamManager.requestedFacingDirection = CameraFacingDirection.World;
                    Debug.Log("KSH Touch 2 : " + ARCamManager.currentFacingDirection);
                }
            }
        }
    }
}
