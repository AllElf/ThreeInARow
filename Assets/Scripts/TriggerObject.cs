using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] ScriptManager manager;
    [SerializeField]private Sprite _sprite;
    [SerializeField] Vector3 scale = new Vector2(1f, 1f);
    private void Start()
    {
        manager = FindAnyObjectByType<ScriptManager>();
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject.GetComponent<TriggerObject>());
        if(collision.gameObject.GetComponent<SpriteRenderer>().sprite == _sprite && collision.gameObject.transform.localScale != scale)
        {
            Destroy(collision.gameObject);
            gameObject.transform.localScale = scale; 
            gameObject.GetComponent<SpriteRenderer>().sprite = manager.currentSprite;
        }
    }
}
