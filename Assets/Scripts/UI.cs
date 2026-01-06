using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] public List<GameObject> spritesUI;
    [SerializeField] Moving moving;
    [SerializeField] bool isPause = false;
    [SerializeField] GameObject panelPause;

  

    private void Update()
    {
        SpriteCycle();
    }
    public void SpriteCycle()
    {
        for (int i = 0; i < spritesUI.Count; i++)
        {
            spritesUI[i].GetComponent<Image>().sprite = moving.spritesUI[i];
        }
    }
    public void Pausa()
    {
        isPause = !isPause;
        if (isPause)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            panelPause.SetActive(false);
        }
    }
}
