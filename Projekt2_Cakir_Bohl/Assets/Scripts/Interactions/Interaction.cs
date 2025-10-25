using System.Linq;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool HasFinished => _hasFinished;
    [SerializeField] protected Transform[] _anchors;

    protected bool _hasFinished;

    public Vector2 ReturnClosestAnchor(Vector3 playerPosition)
    {
        // https://stackoverflow.com/questions/33145365/what-is-the-most-effective-way-to-get-closest-target
        Transform closestAnchor = _anchors.OrderBy(anchor => (anchor.position - playerPosition).sqrMagnitude).FirstOrDefault();

        return new Vector2(closestAnchor.position.x, closestAnchor.position.y);
    }

    protected void ResetHasFinished()
    {
        if(_hasFinished == true)
        {
            _hasFinished = false;
        }
    }
}
