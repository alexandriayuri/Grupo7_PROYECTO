using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CinematicEffect : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSize = 3f;
    public float zoomSpeed = 2f;
    public Vector3 focusOffset = new Vector3(0, 0, 0); // Adjust the offset as needed
    
    private float originalSize;
    private Vector3 originalOffset;
    private bool isZooming = false;
    private CinemachineFramingTransposer framingTransposer;

    void Start()
    {
        originalSize = virtualCamera.m_Lens.OrthographicSize;
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        originalOffset = framingTransposer.m_TrackedObjectOffset;
    }

    void Update()
    {
        if (isZooming)
        {
            // Camera Zoom
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomSize, Time.deltaTime * zoomSpeed);

            // Adjust camera focus
            framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(framingTransposer.m_TrackedObjectOffset, focusOffset, Time.deltaTime * zoomSpeed);
        }


        else
        {
            // Reset Camera
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, originalSize, Time.deltaTime * zoomSpeed);

            // Reset camera focus
            framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(framingTransposer.m_TrackedObjectOffset, originalOffset, Time.deltaTime * zoomSpeed);

        }
    }
    public void TriggerCinematic(bool enable)
    {
        isZooming = enable;
    }
}
