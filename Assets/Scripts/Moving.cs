using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Moving : MonoBehaviour
{
    [SerializeField] int index;
    public Collider2D touchCollider;
    public Transform target;
    Vector2 mouseWorld;
    private bool dragging;
    [SerializeField] GameObject prefap;
    public List<Sprite> sprites;
    public List<Sprite> spritesUI;
    int spriteIndex;
    GameObject pref;

    private void Start()
    {
        spritesUI = sprites;
        index = Random.Range(0, 4);
        SpriteCount();
    }

    void Update()
    {
        if (!touchCollider || !target) return;

         mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (touchCollider.OverlapPoint(mouseWorld))
                dragging = true;
        }
        if (Input.GetMouseButton(0) && dragging)
        {
            Vector2 contactPoint = touchCollider.ClosestPoint(mouseWorld);

            target.position = new Vector3( contactPoint.x,target.position.y,target.position.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
           if(pref !=null)
            {
                pref.transform.SetParent(null);
                pref.GetComponent<Rigidbody2D>().simulated = true;
            }
            StopAllCoroutines();
            StartCoroutine(RandomSprites());

        }
            
    }
    

    public void SpriteCount()
    {
        spritesUI = new List<Sprite>(sprites);

        pref = Instantiate(prefap, target.position, Quaternion.identity, target);
        pref.GetComponent<SpriteRenderer>().sprite = sprites[index];
        pref.GetComponent<Rigidbody2D>().simulated = false;

        Sprite selected = sprites[index];
        sprites.RemoveAt(index);
        sprites.Add(selected);
    }
    IEnumerator  RandomSprites()
    {
        index = Random.Range(0, 4);
        yield return new WaitForSeconds(1f);
        SpriteCount();
    }


}
