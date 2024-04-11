using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BodyTracker2D : MonoBehaviour
{
    ARHumanBodyManager _arHumanBodyManager;

    [SerializeField]
    private GameObject _jointPrefab;
    [SerializeField]
    private Camera _camera;

    private Dictionary<int, GameObject> _jointObjs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        _arHumanBodyManager = GetComponent<ARHumanBodyManager>();
    }

    private void Update()
    {
        NativeArray<XRHumanBodyPose2DJoint> joints = _arHumanBodyManager.GetHumanBodyPose2DJoints(Allocator.Temp);

        if (!joints.IsCreated)
        {
            return;
        }

        UpdateJoints(joints);
    }

    private void UpdateJoints(NativeArray<XRHumanBodyPose2DJoint> joints)
    {
        for(int index = 0; index < joints.Length; index++)
        {
            XRHumanBodyPose2DJoint joint = joints[index];

            GameObject obj;
            if(!_jointObjs.TryGetValue(index, out obj))
            {
                obj = Instantiate(_jointPrefab);
                _jointObjs.Add(index, obj);
            }

            if (joint.tracked)
            {
                obj.transform.position = _camera.ViewportToWorldPoint(new Vector3(joint.position.x, joint.position.y, 2.0f));
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
