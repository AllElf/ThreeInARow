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
    [Header("Бонус за овощ")]
    public int bonus;
   


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
        if (collision.gameObject.tag == "GameOverTrigger")
        {
            Debug.Log("Конец Игры");
            manager.GameOver();
        }
        TriggerObject other = collision.gameObject.GetComponent<TriggerObject>();
        if (!other) return;

        if (other._index != _index) return;
        manager.Distribution();
        internalIndex = manager.countTrigger;
        manager.BallConnection();
        if (other.internalIndex < internalIndex)
        {
            manager.countCoin += other.bonus;
            Destroy(other.gameObject);
        }
        else
        {
            manager.countCoin += bonus;
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
        if(collision.gameObject.tag == "GameOverTrigger")
        {
            Debug.Log("Конец Игры");
        }
        TriggerObject other = collision.gameObject.GetComponent<TriggerObject>();
        if (!other) return;

        if (other._index != _index) return;
        //manager.Distribution();
        internalIndex = manager.countTrigger;
        manager.BallConnection();
        if (other.internalIndex < internalIndex)
        {
            manager.countCoin += other.bonus;
            Destroy(other.gameObject);
        }
        else
        {
            manager.countCoin += bonus;
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
