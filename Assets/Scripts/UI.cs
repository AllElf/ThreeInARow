using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] public List<GameObject> spritesUI;
    [SerializeField] ScriptManager scriptManager;
    [SerializeField] bool isPause = false, isMusic = true;
    [SerializeField] GameObject panelPause;
    [SerializeField] AudioListener audioListener;
    [SerializeField] AudioSource music;

    [Header("Кнопки")]
    [SerializeField] GameObject buttonMenu;
    [SerializeField] GameObject buttonRestart;
    [SerializeField] GameObject buttonContinue;
    public float time;
    bool isActive;

    private void Start()
    {
        audioListener = FindAnyObjectByType<AudioListener>();
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
        //SpriteCycle();
        OrientationView();
        PanelActive();
    }
    //public void SpriteCycle()
    //{
    //    for (int i = 0; i < spritesUI.Count; i++)
    //    {
    //        spritesUI[i].GetComponent<Image>().sprite = scriptManager.spritesUI[i];
    //    }
    //}
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
    public void OrientationView()
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
        if (music == null) return;
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
        if(audioListener == null) return;
        audioListener.enabled = !audioListener.enabled;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
