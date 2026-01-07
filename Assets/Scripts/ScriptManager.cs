using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    [SerializeField] UI uI;
    [SerializeField] int spriteIndex;
    public Collider2D touchCollider;
    public Transform target;
    Vector2 mouseWorld;
    private bool dragging;
    [SerializeField] GameObject prefap;
    public List<Sprite> sprites;
    public List<Sprite> spritesUI;
    GameObject clonePref;

    private void Start()
    {
        spritesUI = sprites;
        spriteIndex = Random.Range(0, 4);
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
        spritesUI = new List<Sprite>(sprites);

        clonePref = Instantiate(prefap, target.position, Quaternion.identity, target);
        clonePref.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
        clonePref.GetComponent<Rigidbody2D>().simulated = false;

        Sprite selected = sprites[spriteIndex];
        sprites.RemoveAt(spriteIndex);
        sprites.Add(selected);
    }
    IEnumerator RandomSprites()
    {
        spriteIndex = Random.Range(0, 4);
        yield return new WaitForSeconds(1f);
        SpriteCount();
    }
}
