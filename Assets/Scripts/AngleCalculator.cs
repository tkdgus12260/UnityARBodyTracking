using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    // 오차 각도 계산 함수
    public float ErrorAngleCalculate(float angle)
    {
        float absErrorAngle = 90f - angle;

        return absErrorAngle;
    }

    // 어깨 기준으로 허리의 각도 계산 함수
    public float PullUpCalculateAngle(Transform leftShoulder, Transform rightShoulder, Transform spine)
    {
        Vector3 centerPoint = (leftShoulder.position + rightShoulder.position) / 2;
        Vector3 spineToCenter = spine.position - centerPoint;
        Vector3 leftShoulderToCenter = leftShoulder.position - centerPoint;

        float angle = Vector3.Angle(spineToCenter, leftShoulderToCenter);

        return angle;
    }
}
