using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToAppear;

    private void Start()
    {
        foreach (GameObject objectToAppear in _objectsToAppear)
        {
            objectToAppear.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject objectToAppear in _objectsToAppear)
            {
                objectToAppear.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject objectToAppear in _objectsToAppear)
            {
                objectToAppear.SetActive(false);
            }
        }
    }
}
