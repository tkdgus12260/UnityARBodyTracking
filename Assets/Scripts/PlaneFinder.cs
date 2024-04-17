using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneFinder : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private BodyTracker3D bodyTracker3D;

    public List<ARPlane> PlaneObjs = new List<ARPlane>();

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        bodyTracker3D = GetComponent<BodyTracker3D>();
    }

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        foreach (ARPlane plane in eventArgs.added)
        {
            PlaneObjs.Add(plane);
        }

        foreach(ARPlane plane1 in eventArgs.removed)
        {
            PlaneObjs.Remove(plane1);
        }
    }
}
