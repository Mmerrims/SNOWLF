using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFace : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
