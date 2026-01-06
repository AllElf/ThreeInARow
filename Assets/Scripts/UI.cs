using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] public List<GameObject> spritesUI;
    [SerializeField] Moving moving;
    [SerializeField] bool isPause = false;
    [SerializeField] GameObject panelPause;
    public float time;


    private void Update()
    {
        Time.timeScale = time;
        SpriteCycle();
        OrientationView();
    }
    public void SpriteCycle()
    {
        for (int i = 0; i < spritesUI.Count; i++)
        {
            spritesUI[i].GetComponent<Image>().sprite = moving.spritesUI[i];
            //spritesUI[i].GetComponent<Image>().sprite = moving.spritesUI[i].sprite;
        }
    }
    public void Pausa()
    {
        isPause = !isPause;
        if (isPause)
        {
            panelPause.SetActive(true);
            time = 0f;
        }
        else
        {
            time = 1f;
            panelPause.SetActive(false);
        }
    }

    public void OrientationView()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||Screen.orientation == ScreenOrientation.LandscapeRight) //Горизонтальная ориентация
        {
            Camera.main.orthographicSize = 5;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) // Вертикальная ориентация
        {
            Camera.main.orthographicSize = 6;
        }

    }
}
