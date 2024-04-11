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

    [SerializeField]
    private Material _lineMaterial;

    private Dictionary<int, GameObject> _jointObjs = new Dictionary<int, GameObject>();
    private Dictionary<int, LineRenderer> _jointLines = new Dictionary<int, LineRenderer>();

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
        DrawLines(joints);
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

    private void DrawLines(NativeArray<XRHumanBodyPose2DJoint> joints)
    {
        foreach (var joint in joints)
        {
            if (!_jointLines.ContainsKey(joint.index))
            {
                GameObject lineObj = new GameObject("LineRenderer");
                lineObj.transform.SetParent(transform);
                LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.02f;
                lineRenderer.endWidth = 0.02f;
                lineRenderer.material = _lineMaterial;
                lineRenderer.positionCount = 2;
                _jointLines.Add(joint.index, lineRenderer);
            }

            int parentIndex = joint.parentIndex;
            if (parentIndex >= 0 && _jointObjs.ContainsKey(parentIndex))
            {
                LineRenderer lineRenderer = _jointLines[joint.index];
                lineRenderer.SetPosition(0, _jointObjs[parentIndex].transform.position);
                lineRenderer.SetPosition(1, _jointObjs[joint.index].transform.position);
            }
        }
    }
}
