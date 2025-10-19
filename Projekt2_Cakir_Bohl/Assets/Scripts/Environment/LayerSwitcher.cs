using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private string _sortingLayerName;
    private int _sortingOrder;

    void Start()
    {
        _sortingLayerName = _renderer.sortingLayerName;
        _sortingOrder = _renderer.sortingOrder;

        // Debug.Log(_sortingLayerName);
        // Debug.Log(_sortingOrder);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Enter");
            _renderer.sortingLayerName = GameConfig.AlternativeSortingLayerName;
            _renderer.sortingOrder = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Exit");
            _renderer.sortingLayerName = _sortingLayerName;
            _renderer.sortingOrder = _sortingOrder;
        }
    }
}
