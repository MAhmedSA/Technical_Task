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

    [Header("Wave Section")]
    [Tooltip("Text Variable for Wave")]
    [SerializeField]
    TextMeshProUGUI _waveText;
    [Tooltip("Text Variable for Enemies count")]
    [SerializeField]
    TextMeshProUGUI _enemiesText;

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
        UpdateTextWave();
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
            GameManager.instance.CanSpwan = false;
            GameManager.instance.CheckCanGenerate();
        }
        else
        {
            //If Wave is Played then changing the Sprite to Pause Sprite
            _buttonPuase.image.sprite = _playAndPauseSprites[0];
            GameManager.instance.CanSpwan = true;
            GameManager.instance.CheckCanGenerate();
        }
    }

    //Function To Update Frame Rate Text Value
    public void UpdateFrameUI(float frameRate) {

        fpsText.text = "FPS: " + (frameRate).ToString("F2");
    }

    public void UpdateTextWave()
    {
        _waveText.text = "Wave: " + GameManager.instance.CurrentWave.ToString();
    }
    public void NextWaveButton() { 

        StartCoroutine(GameManager.instance.NextWave());
       

    }

    public void UpdateTextEnemies(int num) {

        _enemiesText.text = "Enemies: " + num;
    }
    public void DestroyAllEnemies() {
        GameManager.instance.DestroyEnemies();
    }
}
