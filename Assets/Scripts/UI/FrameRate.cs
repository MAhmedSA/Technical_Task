using TMPro;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60; // Set target frame rate to 60 FPS
    }
    void Update()
    {
        //Calculate the FPS
        float fps = 1.0f / Time.unscaledDeltaTime;
        //Update the FPS display in the UI
        UIManager.instance.UpdateFrameUI(fps);
    }
}
