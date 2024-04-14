using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;
    public Transform obj3;

    //private void Update()
    //{
    //    Debug.Log(ErrorAngleCalculate(PullUpCalculateAngle(obj2, obj3, obj1)));
    //}

    public float ErrorAngleCalculate(float angle)
    {
        float absErrorAngle = Mathf.Abs(90f - angle);

        return absErrorAngle;
    }

    public float PullUpCalculateAngle(Transform leftShoulder, Transform rightShoulder, Transform spine)
    {
        Vector3 centerPoint = (leftShoulder.position + rightShoulder.position) / 2;
        Vector3 spineToCenter = spine.position - centerPoint;
        Vector3 leftShoulderToCenter = leftShoulder.position - centerPoint;

        float angle = Vector3.Angle(spineToCenter, leftShoulderToCenter);

        return angle;
    }
}
