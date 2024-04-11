using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARBodyTracker : MonoBehaviour
{
    [SerializeField]
    GameObject bodyPrefab;

    ARHumanBodyManager bodyManager;

    GameObject bodyObject;

    private void Awake()
    {
        bodyManager = GetComponent<ARHumanBodyManager>();
    }

    private void OnEnable()
    {
        bodyManager.humanBodiesChanged += OnHumanBodiesChanged;
    }

    private void OnDisable()
    {
        bodyManager.humanBodiesChanged -= OnHumanBodiesChanged;
    }

    private void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs)
    {
        foreach(ARHumanBody humanBody in eventArgs.added)
        {
            bodyObject = Instantiate(bodyPrefab, humanBody.transform);
        }

        foreach (ARHumanBody humanBody in eventArgs.updated)
        {
            if (bodyObject != null)
            {
                bodyObject.transform.position = humanBody.transform.position;
                bodyObject.transform.rotation = humanBody.transform.rotation;
                bodyObject.transform.localScale = humanBody.transform.localScale;
            }
        }

        foreach(ARHumanBody humanBody in eventArgs.removed)
        {
            if (bodyObject != null)
            {
                Destroy(bodyObject);
            }
        }
    }
}
