using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShadowFade : MonoBehaviour
{
    public Camera MainCamera;
    public Light MainShadowCaster;
    public float minDistance;
    public float fadeDistance;

    private float cameraDistance;
    private float currentDistance;
    private float ShadowValue;
    void Start()
    {
        ShadowValue = MainShadowCaster.shadowStrength;
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(MainCamera.transform.position,Vector3.down,  out hit))
        {
            cameraDistance = hit.distance;
        }

        if (cameraDistance > minDistance)
        {
            currentDistance = Mathf.Clamp(cameraDistance / minDistance, 1, 2);
            float fromMin = currentDistance - 1;
            float fromMax = fadeDistance - fromMin;

            float normal = fromMin / fromMax;

            float toMax = 1 - 0;
            float toAbs = toMax * normal;

            float to = toAbs + 0;
            float distance = Mathf.Lerp(ShadowValue, 0, to);
 
            MainShadowCaster.shadowStrength = distance;
        }
    }
}
