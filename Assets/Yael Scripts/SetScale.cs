using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetScale : MonoBehaviour
{
    [SerializeField] private float scale = 1f;
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private float minScale = 1f;
    private List<Rigidbody> rigidbodies = new ();
    [SerializeField] private float scaleDelay = .1f;
    [SerializeField,Tooltip("Minimum speed that the ball must goes before it scales")] private float SnolfSpeed = .1f;
   // [SerializeField,Tooltip("Minimum speed that the ball must increase before the scale rate increases")] private float maxSnolfSpeed = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rigidbodies.Add(rb);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rigidbodies.Remove(rb);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ScaleOverTime());
    }

    IEnumerator ScaleOverTime()
    {
        while (gameObject.activeSelf)
        {
            foreach (var rb in rigidbodies)
            {
                if( rb.velocity.magnitude < SnolfSpeed)
                {
                    continue;
                }
                Vector3 snolfScale = rb.transform.localScale;
                print("before, " + snolfScale);
                float sizeController = Mathf.Clamp(snolfScale.x + (scaleDelay * scale), minScale, maxScale);
                snolfScale = new Vector3(sizeController, sizeController, sizeController);
                rb.transform.localScale = snolfScale;
                print("after, " + snolfScale);
            }

            yield return new WaitForSeconds(scaleDelay);
        }
    }
}
