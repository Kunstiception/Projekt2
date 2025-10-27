using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private ItemContainer _itemContainer;

    public Item SetItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, _itemContainer.PossibleItems.Length);
        GameObject instance = _itemContainer.PossibleItems[randomIndex];

        Instantiate(instance, transform.position, Quaternion.identity);

        return instance.GetComponent<Item>();
    }
}
