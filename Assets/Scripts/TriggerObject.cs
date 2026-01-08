using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [Header("Ссылка")]
    [SerializeField] ScriptManager manager;
    [Header("Настройки спрайта")]
    public Sprite _sprite;
    SpriteRenderer rendererSprite;
    [Header("Внутренняя и наружная индексация")]
    public int _index; 
    public int internalIndex;
    
    private void Start()
    {
        manager = FindAnyObjectByType<ScriptManager>();
        rendererSprite = GetComponent<SpriteRenderer>();
        if (manager!= null && _sprite != null && rendererSprite!=null)
        {
            rendererSprite.sprite = _sprite;
            _index = manager.currentIndex;
            gameObject.transform.localScale = manager.indexSprite[_index].scale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerObject other = collision.gameObject.GetComponent<TriggerObject>();
        if (!other) return;

        if (other._index != _index) return;
        manager.Distribution();
        internalIndex = manager.countTrigger;
        if(other.internalIndex < internalIndex)
        {
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //gameObject.transform.localScale = scale;
        _index++;
        if(_index <= manager.indexSprite.Count)
        {
            rendererSprite.sprite = manager.indexSprite[_index].image.sprite;
            gameObject.transform.localScale = manager.indexSprite[_index].scale;
        } 
    }
    private void OnCollisionStay(Collision collision)
    {
        TriggerObject other = collision.gameObject.GetComponent<TriggerObject>();
        if (!other) return;

        if (other._index != _index) return;
        //manager.Distribution();
        internalIndex = manager.countTrigger;
        if (other.internalIndex < internalIndex)
        {
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _index++;
        if (_index <= manager.indexSprite.Count)
        {
            rendererSprite.sprite = manager.indexSprite[_index].image.sprite;
            gameObject.transform.localScale = manager.indexSprite[_index].scale;
        }
    }
}
