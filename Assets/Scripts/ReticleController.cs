using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    private float zOffset;
    private RaycastHit hitInfo;

    void Awake()
    {
        zOffset = transform.localPosition.z;
    }

    void Update()
    {
        transform.localPosition = Vector3.forward * zOffset;

        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hitInfo, zOffset))
        {
            if (hitInfo.transform != transform)
            {
                transform.localPosition = Vector3.forward * hitInfo.distance;
                float newScale = hitInfo.distance * 0.01f / 10.0f;
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
        }
    }
}
