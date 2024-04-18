using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BodyTracker3D : MonoBehaviour
{
    // ARkit 3D Skeleton Joints Index
    enum JointIndices
    {
        Invalid = -1,
        Root = 0, // parent: <none> [-1]
        Hips = 1, // parent: Root [0]
        LeftUpLeg = 2, // parent: Hips [1]
        LeftLeg = 3, // parent: LeftUpLeg [2]
        LeftFoot = 4, // parent: LeftLeg [3]
        LeftToes = 5, // parent: LeftFoot [4]
        LeftToesEnd = 6, // parent: LeftToes (5)
        RightUpLeg = 7, // parent: Hips [1]
        RightLeg = 8, // parent: RightUpLeg_[7]
        RightFoot = 9, // parent: RightLeg [8]
        RightToes = 10, // parent: RightFoot [9]
        RightToesEnd = 11, // parent: RightToes [10]
        Spine1 = 12, // parent: Hips [1]
        Spine2 = 13, // parent: Spine1 [12]
        Spine3 = 14, // parent: Spine2 [13]
        Spine4 = 15, // parent: Spine3 [14]
        Spine5 = 16, // parent: Spine4 [15]
        Spine6 = 17, // parent: Spine5 [16]
        Spine7 = 18, // parent: Spine6 [17]
        LeftShoulder1 = 19, // parent: Spine7 [18]
        LeftArm = 20, // parent: LeftShoulder1 [19]
        LeftForearm = 21, // parent: LeftArm [20)
        LeftHand = 22, // parent: LeftForearm (21)
        LeftHandIndexStart = 23, // parent: LeftHand [22]
        LeftHandIndex1 = 24, // parent: LeftHandIndexStart [23]
        LeftHandIndex2 = 25, // parent: LeftHandIndex1 [24]
        LeftHandIndex3 = 26, // parent: LeftHandIndex2 [25]
        LeftHandIndexEnd = 27, // parent: LeftHandIndex3 [26]
        LeftHandMidStart = 28, // parent: LeftHand [22]
        LeftHandMid1 = 29, // parent: LeftHandMidStart [28)
        LeftHandMid2 = 30, // parent: LeftHandMidi [29]
        LeftHandMid3 = 31, // parent: LeftHandMid2 [30]
        LeftHandMidEnd = 32, // parent: LeftHandMid3 [31]
        LeftHandPinkyStart = 33, // parent: LeftHand [22]
        LeftHandPinky1 = 34, // parent: LeftHandPinkyStart [33]
        LeftHandPinky2 = 35, // parent: LeftHandPinky1 [34]
        LeftHandPinky3 = 36, // parent: LeftHandPinky2 [35]
        LeftHandPinkyEnd = 37, // parent: LeftHandPinky3 [36]
        LeftHandRingStart = 38, // parent: LeftHand [22]
        LeftHandRing1 = 39, // parent: LeftHandRingStart [38]
        LeftHandRing2 = 40, // parent: LeftHandRing1 [39]
        LeftHandRing3 = 41, // parent: LeftHandRing2 [49]
        LeftHandRingEnd = 42, // parent: LeftHandRing3 [41]
        LeftHandThumbStart = 43, // parent: LeftHand [22]
        LeftHandThumb1 = 44, // parent: LeftHandThumbStart [43]
        LeftHandThumb2 = 45, // parent: LeftHandThumb1 [44]
        LeftHandThumbEnd = 46, // parent: LeftHandThumb2 [45]
        Neck1 = 47, // parent: Spine7 [18]
        Neck2 = 48, // parent: Neck1 [47]
        Neck3 = 49, // parent: Neck2 [48]
        Neck4 = 50, // parent: Neck3 [49]
        Head = 51, // parent: Neck4 [50]
        Jaw = 52, // parent: Head [51]
        Chin = 53, // parent: Jaw [52]
        LeftEye = 54, // parent: Head [51]
        LeftEyeLowerLid = 55, // parent: LeftEye [54]
        LeftEyeUpperLid = 56, // parent: LeftEye [54]
        LeftEyeball = 57, // parent: LeftEye [54]
        Nose = 58, // parent: Head [51]
        RightEye = 59, // parent: Head [51)
        RightEyeLowerLid = 60, // parent: RightEye [59]
        RightEyeUpperLid = 61, // parent: RightEye [59]
        RightEyeball = 62, // parent: RightEye [59]
        RightShoulder1 = 63, // parent: Spine7 [18]
        RightArm = 64, // parent: RightShoulder1 [63]
        RightForearm = 65, // parent: RightArm [64]
        RightHand = 66, // parent: RightForearm [65]
        RightHandIndexStart = 67, // parent: RightHand [66]
        RightHandIndex1 = 68, // parent: RightHandIndexStart [67]
        RightHandIndex2 = 69, // parent: RightHandIndex1 [68]
        RightHandIndex3 = 70, // parent: RightHandIndex2 [69]
        RightHandIndexEnd = 71, // parent: RightHandIndex3 [78]
        RightHandMidStart = 72, // parent: RightHand [66]
        RightHandMid1 = 73, // parent: RightHandMidStart [72]
        RightHandMid2 = 74, // parent: RightHandMid1 [73]
        RightHandMid3 = 75, // parent: RightHandMid2 [74]
        RightHandMidEnd = 76, // parent: RightHandMid3 [75]
        RightHandPinkyStart = 77, // parent: RightHand [66]
        RightHandPinky1 = 78, // parent: RightHandPinkyStart [77]
        RightHandPinky2 = 79, // parent: RightHandPinky1 [78]
        RightHandPinky3 = 80, // parent: RightHandPinky2 [79]
        RightHandPinkyEnd = 81, // parent: RightHandPinky3 [80]
        RightHandRingStart = 82, // parent: RightHand [66]
        RightHandRing1 = 83, // parent: RightHandRingStart_ [82]
        RightHandRing2 = 84, // parent: RightHandRing1 [83]
        RightHandRing3 = 85, // parent: RightHandRing2 [84]
        RightHandRingEnd = 86, // parent: RightHandRing3 [85]
        RightHandThumbStart = 87, // parent: RightHand [66]
        RightHandThumb1 = 88, // parent: RightHandThumbStart [87]
        RightHandThumb2 = 89, // parent: RightHandThumb1 [88]
        RightHandThumbEnd = 90, // parent: RightHandThumb2 [89]
    }

    private ARHumanBodyManager arHumanManager;
    private AngleCalculator angleCalculator;
    private PlaneFinder planeFinder;

    private ARPlane arPlane;

    [SerializeField]
    Text text;

    [SerializeField]
    Text textPlane;

    [SerializeField]
    private GameObject jointPrefab;
    [SerializeField]
    private Material lineMat;
    [SerializeField]
    private Material planeMaterial;

    private Transform leftShoulder;
    private Transform rightShoulder;
    private Transform spine;
    private Transform rightFoot;
    private Transform leftFoot;
    private Transform rightForearm;

    private Dictionary<int, GameObject> jointObjs = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> lineObjs = new Dictionary<int, GameObject>();

    private int count = 0;
    private bool isPullUpStarted = false;
    private bool isPullUpEnded = false;
    private bool status = false;

    private void Awake()
    {
        arHumanManager = GetComponent<ARHumanBodyManager>();
        angleCalculator = GetComponent<AngleCalculator>();
        planeFinder = GetComponent<PlaneFinder>();
    }

    private void OnEnable()
    {
        arHumanManager.humanBodiesChanged += OnHumanBodyChanged;
    }

    private void OnDisable()
    {
        arHumanManager.humanBodiesChanged -= OnHumanBodyChanged;
    }

    // Body Tracking 인식 시 실행되는 이벤트 함수
    private void OnHumanBodyChanged(ARHumanBodiesChangedEventArgs eventArgs)
    {
        foreach (ARHumanBody humanBody in eventArgs.updated)
        {
            NativeArray<XRHumanBodyJoint> joints = humanBody.joints;

            foreach (XRHumanBodyJoint joint in joints)
            {
                // 조인트 인덱스가 1~22, 51~52, 63~66 범위 내에 있는 경우에만 처리.
                if ((joint.index >= 1 && joint.index <= 22) || joint.index == 51 || joint.index == 50 || (joint.index >= 63 && joint.index <= 66))
                {
                    GameObject obj;
                    if (!jointObjs.TryGetValue(joint.index, out obj))
                    {
                        obj = Instantiate(jointPrefab);

                        switch (joint.index)
                        {
                            case 4:
                                leftFoot = obj.transform;
                                break;
                            case 9:
                                rightFoot = obj.transform;
                                break;
                            case 12:
                                spine = obj.transform;
                                obj.GetComponent<Renderer>().material.color = Color.red;
                                break;
                            case 19:
                                leftShoulder = obj.transform;
                                obj.GetComponent<Renderer>().material.color = Color.red;
                                break;
                            case 63:
                                rightShoulder = obj.transform;
                                obj.GetComponent<Renderer>().material.color = Color.red;
                                break;
                            case 65:
                                rightForearm = obj.transform;
                                break;
                        } 

                        jointObjs.Add(joint.index, obj);
                    }

                    if (joint.tracked)
                    {
                        obj.transform.parent = humanBody.transform;
                        // estimatedHeightScaleFactor = 기본 body height가 정의되어 있는데, 키는 상대적이라 키를 예측해서 ScaleFactor를 저장한 값.
                        obj.transform.localPosition = joint.anchorPose.position * humanBody.estimatedHeightScaleFactor;
                        obj.transform.localRotation = joint.anchorPose.rotation;
                        obj.SetActive(true);

                        if (jointObjs.TryGetValue(joint.parentIndex, out GameObject parentObj))
                        {
                            DrawLineBetweenJoints(parentObj.transform.position, obj.transform.position, joint.index);
                        }

                        float angle = angleCalculator.PullUpCalculateAngle(leftShoulder, rightShoulder, spine);
                        float absErrorAngle = angleCalculator.ErrorAngleCalculate(angle);

                        text.text = angle.ToString();

                        // 옳지 못한 자세 비율 +-3도
                        if(absErrorAngle >= 3f)
                        {

                        }

                        if(!status)
                            BottomPlane();
                    }
                    else
                    {
                        obj.SetActive(false);
                        DestroyLine(joint.index);
                    }
                }
            }
        }
    }

    private void DrawLineBetweenJoints(Vector3 start, Vector3 end, int jointIndex)
    {
        GameObject lineObj;
        LineRenderer lineRenderer;

        if (!lineObjs.TryGetValue(jointIndex, out lineObj))
        {
            lineObj = new GameObject("Line");
            lineObj.transform.parent = transform;

            lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.material = lineMat;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;

            lineObjs.Add(jointIndex, lineObj);
        }
        else
        {
            lineRenderer = lineObj.GetComponent<LineRenderer>();
        }

        lineRenderer.SetPositions(new Vector3[] { start, end });

        lineObj.SetActive(true);
    }

    private void DestroyLine(int jointIndex)
    {
        GameObject lineObj;
        if (lineObjs.TryGetValue(jointIndex, out lineObj))
        {
            lineObj.SetActive(false);
        }
    }

    public void PullUpRecordingStart()
    {
        BottomPlane();
    }

    private void BottomPlane()
    {
        status = true;
        if (rightFoot == null || rightForearm == null || rightShoulder == null)
            return;

        float closestDistance = float.MaxValue;

        foreach (ARPlane plane in planeFinder.PlaneObjs)
        {
            float distanceToPlane = Mathf.Abs(rightFoot.position.y - plane.transform.position.y);
            if (distanceToPlane < closestDistance)
            {
                closestDistance = distanceToPlane;
                arPlane = plane;
            }
        }

        if (arPlane == null)
            return;

        // 양 발이 바닥에서 떨어졌을 때 시작
        if (FeetLeaveGround())
        {

            // 턱걸이 시작 시
            if (!isPullUpStarted && rightForearm.position.y > rightShoulder.position.y)
            {
                isPullUpStarted = true;
            }
            // 턱걸이 종료 시
            else if (isPullUpStarted && rightForearm.position.y < rightShoulder.position.y)
            {
                isPullUpEnded = true;
            }

            // 턱걸이가 시작되고 종료될 때 count를 증가시킴
            if (isPullUpStarted && isPullUpEnded)
            {
                count++;
                isPullUpStarted = false; // 초기화
                isPullUpEnded = false; // 초기화
            }

            textPlane.text = closestDistance.ToString() + "\n 횟수 : " + count;
        }
        else
        {
            count = 0;
            textPlane.text = closestDistance.ToString() + "\n 횟수 : " + count;
        }

        status = false;
    }

    // 양발이 바닥에서 떨어졌는지 감지
    private bool FeetLeaveGround()
    {
        float LeaveDistance = 0.1f;
        float distanceRightFoot = Mathf.Abs(rightFoot.position.y - arPlane.transform.position.y);
        float distanceLeftFoot = Mathf.Abs(leftFoot.position.y - arPlane.transform.position.y);

        return distanceRightFoot > LeaveDistance && distanceLeftFoot > LeaveDistance;
    }
}
