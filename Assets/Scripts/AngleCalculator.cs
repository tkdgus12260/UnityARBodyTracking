using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;
    public Transform obj3;

    private void Update()
    {
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        Vector3 centerPoint = (obj2.position + obj3.position) / 2; // obj2와 obj3의 중심점 계산
        Vector3 obj1Direction = obj1.position - centerPoint; // obj1에서 중심점을 향하는 벡터 계산
        Vector3 obj2Direction = obj2.position - centerPoint; // obj2에서 중심점을 향하는 벡터 계산

        float angle = Vector3.Angle(obj1Direction, obj2Direction); // obj1과 obj2 방향 사이의 각도 계산
        Debug.Log("KSH : " + angle);
    }
}
