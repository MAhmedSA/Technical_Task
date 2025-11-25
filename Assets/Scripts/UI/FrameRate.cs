using TMPro;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    void Update()
    {
        //Calculate the FPS
        float fps = 1.0f / Time.unscaledDeltaTime;
        //Update the FPS display in the UI
        UIManager.instance.UpdateFrameUI(fps);
    }
}
