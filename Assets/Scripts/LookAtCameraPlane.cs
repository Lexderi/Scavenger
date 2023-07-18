using UnityEngine;

public class LookAtCameraPlane : MonoBehaviour
{
    // references
    private Transform mainCamTransform;

    private void Start() => mainCamTransform = Camera.main!.transform;

    private void LateUpdate() => transform.eulerAngles = mainCamTransform.eulerAngles;
}