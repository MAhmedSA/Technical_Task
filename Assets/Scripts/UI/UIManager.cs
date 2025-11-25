using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Pause And Play Button")]
    [Tooltip("Reference for Pause And Play Button")]
    [SerializeField]
     Button _buttonPuase;
    [Tooltip("Sprite Array for Play & Pause Sprite")]
    [SerializeField] Sprite[] _playAndPauseSprites;
    //boolean to check Wave Of Enemy is paused or not
    bool _isPaused = false;

    [Header("FPS Section")]
    [Tooltip("Text Variable for FPS")]
    [SerializeField]
    TextMeshProUGUI fpsText;

    //SingleTon Instance
    public static UIManager instance;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //Adding Listener to Pause And Play Button
        _buttonPuase.onClick.AddListener(TogglePauseAndPlay);
    }

    public void TogglePauseAndPlay()
    {
        //Toggling the boolean value
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            //If Wave is Paused then changing the Sprite to Play Sprite
            _buttonPuase.image.sprite = _playAndPauseSprites[1];
        }
        else
        {
            //If Wave is Played then changing the Sprite to Pause Sprite
            _buttonPuase.image.sprite = _playAndPauseSprites[0];
        }
    }

    //Function To Update Frame Rate Text Value
    public void UpdateFrameUI(float frameRate) {

        fpsText.text = "FPS: " + (frameRate).ToString("F2");
    }
}
