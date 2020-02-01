using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject defaultCamera;
    public GameObject runCamera;
    public int activeCamera;
    public enum CameraState { Default, TransitionToRun, Run, TransitionToDefault };

    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.SetActive(true);
        runCamera.SetActive(false);
        activeCamera = (int)CameraState.Default;
    }

    public void SwitchToDefaultCamera()
    {
        defaultCamera.SetActive(true);
        runCamera.SetActive(false);
        activeCamera = (int)CameraState.Default;
    }

    public void SwitchToRunCamera()
    {
        defaultCamera.SetActive(false);
        runCamera.SetActive(true);
        activeCamera = (int)CameraState.Run;
    }
}
