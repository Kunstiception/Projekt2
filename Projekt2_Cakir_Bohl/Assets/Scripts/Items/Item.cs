using System;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform[] _anchors;

    public static event Action<Item> onItemEnter;
    public static event Action OnItemExit;

    private void OnMouseEnter()
    {
        onItemEnter?.Invoke(this);
    }

    private void OnMouseExit()
    {
        OnItemExit?.Invoke();
    }

    public Vector2 ReturnClosestAnchor(Vector3 playerPosition)
    {
        // https://stackoverflow.com/questions/33145365/what-is-the-most-effective-way-to-get-closest-target
        Transform closestAnchor = _anchors.OrderBy(anchor => (anchor.position - playerPosition).sqrMagnitude).FirstOrDefault();

        return new Vector2(closestAnchor.position.x, closestAnchor.position.y);
    }
}
