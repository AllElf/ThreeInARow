using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UI : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] ScriptManager scriptManager;
    //[SerializeField] AudioListener audioListener;
    

    [Header("Логические переменные")]
    [SerializeField] bool isPause = false;
    [SerializeField] bool ishearing = true;
    [SerializeField] bool isMusic = true;

    bool isActive;

    [Header("Свойство и настройка панели паузы")]
    [SerializeField] GameObject panelPause;
    public float time;
   

    [Header("Кнопки")]
    [SerializeField] GameObject buttonMenu;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] GameObject buttonContinue;

    [Header("Настройка звука")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource stack;

    private AudioMixerGroup musicGroup; 
    private AudioMixerGroup stackGroup;

    private void Start()
    {
        audioMixer = Resources.Load<AudioMixer>("Main");
        musicGroup = audioMixer.FindMatchingGroups("Music")[0]; 
        stackGroup = audioMixer.FindMatchingGroups("Stack")[0];

        scriptManager = FindAnyObjectByType<ScriptManager>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        isActive = true;
        OneStrtAction();
        Pausa();   
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
            Camera.main.orthographicSize = 4.3f;
            panelPause.transform.localScale =  new Vector3(1f,1f,1f);
        }
        else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) // Вертикальная ориентация
        {
            Camera.main.orthographicSize = 6;
            panelPause.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }

    }

    public void Music()
    {
        if (audioMixer == null) return;
        isMusic = !isMusic;

        if(isMusic)
        {
            music.Play();
            
        }
        if (!isMusic)
        {
            music.Stop();
        }
    }
    public void Sound()
    {
        if(audioMixer == null) return;
        ishearing = !ishearing;
        if (ishearing)
        {
            audioMixer.SetFloat("Main", 0f); 
        }
        else
        {
            audioMixer.SetFloat("Main", -80f); 
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
