using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorcontrol : MonoBehaviour
{
    private Vector3 InitialScale;
    private Vector3 InitialPosition;
    public Vector3 OpenPosition = new Vector3(-0.200000003f,2.53999996f,1.25170004e-06f);

    public Vector3 OpenScale = new Vector3(1f, 0.0937480032f, 1f);
    //Vector3(-14.2500086,2.41000009,-6.81660026e-08)
    public void Start()
    {
        InitialScale = transform.localScale;
        InitialPosition = transform.localPosition;
    }
    public void CloseDoor()
    {
        ScaleToTarget(InitialScale,InitialPosition, 1);
    }
    public void OpenDoor()
    {
        ScaleToTarget(OpenScale,OpenPosition, 1);
    }

    public void ScaleToTarget(Vector3 targetScale,Vector3 targetPosition, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(targetScale,targetPosition, duration));
    }

    private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale,Vector3 targetPosition, float duration)
    {
        Vector3 startScale = transform.localScale;
        Vector3 startPosition = transform.localPosition;

        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            //transform.localPosition = Vector3()
            yield return null;
        }

        yield return null;
    }
}
