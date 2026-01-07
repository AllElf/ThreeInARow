using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IndexSprite
{
    public int index;
    public Sprite sprite;
}
public class ScriptManager : MonoBehaviour
{
    [SerializeField] UI uI;
    public int spriteCurrentIndex;
    public Collider2D touchCollider;
    public Transform target;
    Vector2 mouseWorld;
    private bool dragging;
    [SerializeField] GameObject prefap;

    public List<IndexSprite> indexSprite = new List<IndexSprite>();

    //public List<Sprite> sprites;
    public List<GameObject> sprites;
    public Sprite currentSprite;
    GameObject clonePref;

    private void Start()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            indexSprite.Add(new IndexSprite { index = i, sprite = sprites[i].GetComponent<Image>().sprite });
        }
        spriteCurrentIndex = Random.Range(0, 4);
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
                clonePref.transform.SetParent(null);
                clonePref.GetComponent<Rigidbody2D>().simulated = true;
            }
            StopAllCoroutines();
            StartCoroutine(RandomSprites());
        }
    }

    public void SpriteCount()
    {
        //for(int i = 0; i < indexSprite.Count; i++)
        //{
        //    spritesUI[i] = indexSprite[i].sprite;
        //}

        clonePref = Instantiate(prefap, target.position, Quaternion.identity, target);
        clonePref.GetComponent<SpriteRenderer>().sprite = currentSprite;
        clonePref.GetComponent<Rigidbody2D>().simulated = false;

        //IndexSprite elementAdd = indexSprite[spriteCurrentIndex];
        //indexSprite.RemoveAt(spriteCurrentIndex);
        //indexSprite.Add(elementAdd);
    }
    IEnumerator RandomSprites()
    {
        
        spriteCurrentIndex = Random.Range(0, 4);
        currentSprite = indexSprite[Random.Range(0, indexSprite.Count)].sprite;
        yield return new WaitForSeconds(1f);
        SpriteCount();
    }
}
