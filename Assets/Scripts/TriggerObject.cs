using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField]private Sprite _sprite;
    [SerializeField] Vector3 scale = new Vector2(0.64f, 0.64f);
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject.GetComponent<TriggerObject>());
        if(collision.gameObject.GetComponent<SpriteRenderer>().sprite == _sprite && collision.gameObject.transform.localScale != scale)
        {
            Destroy(collision.gameObject);
            gameObject.transform.localScale = scale; 
        }
    }
}
