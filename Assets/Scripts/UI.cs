using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] ScriptManager scriptManager;
    [SerializeField] Camera cam;

    [Header("Визуализация счёта")]
    [SerializeField] Text text;
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject pointTextGameOver;

    [Header("Логические переменные")]
    [SerializeField] bool isPause = false;


    bool isActive;

    [Header("Свойство и настройка панели паузы")]
    public GameObject panelPause;
    
    public float time;
   

    [Header("Кнопки")]
    [SerializeField] GameObject buttonMenu;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] GameObject buttonContinue;
    public GameObject buttonSound;



    private void Awake()
    {
        cam = Camera.main;
        ObjectFind();
    }
    private void Start()
    {
        scriptManager = FindAnyObjectByType<ScriptManager>();
        isActive = true;
        OneStrtAction();
        Pausa();   
    }

    void ObjectFind()
    {
        textObject = GameObject.FindGameObjectWithTag("TextCount");
        text = textObject.GetComponent<Text>();
        pointTextGameOver = GameObject.FindGameObjectWithTag("CountPointGameOver");
        panelPause = GameObject.FindGameObjectWithTag("PanelPause");
        buttonSound = GameObject.FindGameObjectWithTag("Button(Sound)");
        buttonMenu = GameObject.FindGameObjectWithTag("Button(Pausa)");
        buttonRestart = GameObject.FindGameObjectWithTag("Button(Restart)");
        buttonContinue = GameObject.FindGameObjectWithTag("Button(Continue)");
    }
    void AccountVisibility()
    {
        if (text != null)
        {
            text.text = $"Счёт: {scriptManager.countCoin.ToString()}";
        }
    }
    public void GameOverPanel()
    {
        if(buttonContinue != null && buttonRestart != null)
        {
            buttonRestart.transform.position = buttonContinue.transform.position;
            buttonContinue.SetActive(false);
            if(textObject != null && pointTextGameOver != null)
            {
                textObject.transform.SetParent(panelPause.transform);
                textObject.transform.position = pointTextGameOver.transform.position;
            }
            
        }
    }
    void OneStrtAction()
    {
        if (buttonMenu == null || buttonRestart == null || buttonContinue == null) return;
        
        if (isActive)
        {
            buttonMenu.SetActive(false);
            buttonRestart.SetActive(false);
            buttonContinue.GetComponentInChildren<Text>().text = "начать";
        }
        if (!isActive)
        {
            buttonMenu.SetActive(true);
            buttonRestart.SetActive(true);
            buttonContinue.GetComponentInChildren<Text>().text = "продолжить";
        }
    }
    private void Update()
    {
        Time.timeScale = time;
        OrientationView();
        PanelActive();
        AccountVisibility();
    }
    public void Pausa()
    {
        isPause = true;
    }
    public void Continue()
    {
        isActive = false;
        OneStrtAction();
        isPause = false;
    }
    void PanelActive()
    {
        if (panelPause == null) return;

        panelPause.SetActive(isPause);

        if (panelPause.activeSelf)
        {
            time = 0f;
        }
        else if (!panelPause.activeSelf)
        {
            time = 1f;
        }
    }
    private void OrientationView()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||Screen.orientation == ScreenOrientation.LandscapeRight) //Горизонтальная ориентация
        {
            if(panelPause != null && cam != null)
            {
                cam.orthographicSize = 4.3f;
                panelPause.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) // Вертикальная ориентация
        {
            if (panelPause != null && cam != null)
            {
                cam.orthographicSize = 6;
                panelPause.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }    
        }
    }

    
    
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
