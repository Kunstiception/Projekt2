using System.Collections;
using UnityEngine;

public class Line : MonoBehaviour
{
    private float _timer;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        MouseDraw.OnMouseUp += StartFadeOut;
    }
    
    private void OnDisable()
    {
        MouseDraw.OnMouseUp -= StartFadeOut;
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeOut()
    {
        float alpha = 1;

        while (alpha > 0)
        {
            _timer += Time.deltaTime;

            // https://stackoverflow.com/questions/44933517/fading-in-out-gameobject
            alpha = Mathf.Lerp(1, 0, _timer / GameConfig.LineLifeTime);

            //Debug.Log(alpha);

            _lineRenderer.startColor = new Color(_lineRenderer.startColor.r, _lineRenderer.startColor.g, _lineRenderer.startColor.b, alpha);
            _lineRenderer.endColor = new Color(_lineRenderer.endColor.r, _lineRenderer.endColor.g, _lineRenderer.endColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}
