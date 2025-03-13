/*******************************************************************
// File Name :         SetScale.cs
// Author :            Yael Martoral
// Creation Date :     3/11/2025
//
// Brief Description : It controls actions that player can take outside
// of the main control scheme of controlling the snolf ball
/********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SetScale : MonoBehaviour
{
    [SerializeField,Tooltip("Scale that the ball starts when the scene is loaded")] private float scale = 1f;
    [SerializeField,Tooltip("The maximum that the ball can grow too")] private float maxScale = 5f;
    [SerializeField,Tooltip("The minimum that the ball can shrink too")] private float minScale = 1f;
    private List<Rigidbody> rigidbodies = new ();
    [SerializeField,Tooltip("Time before the snolf ball can scale again")] private float scaleDelay = .1f;
    [SerializeField,Tooltip("Minimum speed that the ball must goes before it scales")] private float SnolfSpeed = .1f;
    [SerializeField,Tooltip("The scale of the speed increases once the snolf ball increase its speed")] private float maxScaleSpeed = 8f;
    [SerializeField,Tooltip("Determine if an object uses the speed variable to change the snolf ball")] private bool useSpeed;
    [SerializeField,Tooltip("It decided the type of area of the object in the inspector")] private ScaleChangerType scaleType;

    //Once a Rigidbody enters a trigger, it changes the respective Rigidbody
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rigidbodies.Add(rb);
        }   
    }

    //Once the Rigidbody exists the trigger, it stops altering the Rigidbody
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rigidbodies.Remove(rb);
        }
    }

    //It enables the stated courotines
    private void OnEnable()
    {
        StartCoroutine(ScaleOverTime());
    }

    //A coroutine that controls the scale of the ball depending on what type of area the snolf area is in and alters it's scale acordingly
    IEnumerator ScaleOverTime()
    {
        while (gameObject.activeSelf)
        {
            foreach (var rb in rigidbodies)
            {
                if(rb.velocity.magnitude < SnolfSpeed)
                {
                    continue;
                }
                Vector3 snolfScale = rb.transform.localScale;
                print("before, " + snolfScale);
                float speedscale = useSpeed ? Mathf.Clamp(rb.velocity.magnitude, 0, maxScaleSpeed) : 1;
                float sizeController = Mathf.Clamp(snolfScale.x + (scaleDelay * scale *speedscale), minScale, maxScale);
                snolfScale = new Vector3(sizeController, sizeController, sizeController);
                rb.transform.localScale = snolfScale;
                print("after, " + snolfScale);
                if(scaleType == ScaleChangerType.FIRE && sizeController <= minScale)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            yield return new WaitForSeconds(scaleDelay);
        }
    }


}

public enum ScaleChangerType
{
    SNOW,
    SALT,
    FIRE,
    LAVA
}
