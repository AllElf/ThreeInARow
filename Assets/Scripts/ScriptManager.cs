using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IndexSprite
{
    public int index;
    public Image image;
    public Vector3 scale;
    public int coins;
}
public class ScriptManager : MonoBehaviour
{
    [Header("Счёт")]
    public int countCoin;

    [Header("Ссылка на UI")]
    [SerializeField] UI uI;

    [Header("Настройки передвижения ")]
    public Collider2D touchCollider;
    public Transform target;
    Vector2 mouseWorld;
    private bool dragging;

    [Header("Настройки клона")]
    [SerializeField] GameObject prefap;
    public Sprite currentSprite;
    GameObject clonePref;

    [Header("Индексирование")]
    public int currentIndex;
    public int countTrigger;

    [Header("Список объектов с параметрами")]
    public List<IndexSprite> indexSprite = new List<IndexSprite>();
    
    [Header("Общие звуки")]
    [SerializeField] AudioSource ballConnection;
    [SerializeField] AudioSource ballDrop;
    [SerializeField] AudioClip ballDropClip;
    [SerializeField] AudioSource gameOver;

    bool isCorutine = false;

    public void BallConnection()
    {
        if (ballConnection == null) return;
        if (ballConnection.isPlaying) return;
        ballConnection.Play();
    }
    public void BallDrop()
    {
        if (ballDrop == null) return;
        
        //ballDrop.Play();
        ballDrop.PlayOneShot(ballDropClip);

    }
    public void GameOver()
    {
        if (gameOver == null) return;
        if (gameOver.isPlaying) return;
        gameOver.Play();
        uI.Pausa();
        uI.GameOverPanel();
    }

    private void Start()
    {
        currentIndex = Random.Range(0, 4);
        SpriteCount();
    }

    void Update()
    {
        if (!touchCollider || !target || uI.time != 1f) return;
        if (Input.GetMouseButton(0))
        {
            mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (touchCollider.OverlapPoint(mouseWorld))
                dragging = true;
        }
        if (dragging)
        {
            Vector2 contactPoint = touchCollider.ClosestPoint(mouseWorld);
            target.position = new Vector3(contactPoint.x, target.position.y, target.position.z);
        }
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            if (clonePref != null)
            { 
                BallDrop();
                clonePref.transform.SetParent(null);
                clonePref.GetComponent<Rigidbody2D>().simulated = true;
                clonePref.GetComponent<CircleCollider2D>().isTrigger = false;
               
            }
            if(!isCorutine)
            {
                StartCoroutine(RandomSprites());
            }
        }
    }

    public void SpriteCount()
    {
        clonePref = Instantiate(prefap, target.position, Quaternion.identity, target);
        clonePref.transform.localScale = indexSprite[currentIndex].scale;
        clonePref.GetComponent<TriggerObject>()._sprite = indexSprite[currentIndex].image.sprite;
        clonePref.GetComponent<TriggerObject>()._index = indexSprite[currentIndex].index;
        clonePref.GetComponent<TriggerObject>().bonus = indexSprite[currentIndex].coins;
        clonePref.GetComponent<Rigidbody2D>().simulated = false;
        clonePref.GetComponent<CircleCollider2D>().isTrigger = true;
    }
    IEnumerator RandomSprites()
    {
        isCorutine = true;
        yield return new WaitForSeconds(1f);
        currentIndex = Random.Range(0, 4);
        SpriteCount();
        isCorutine = false;
    }
    public void Distribution()
    {
        countTrigger++;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (indexSprite == null) return;

        for (int i = 0; i < indexSprite.Count; i++)
        {
            if (indexSprite[i] != null)
                indexSprite[i].index = i;
        }
    }
#endif

}
