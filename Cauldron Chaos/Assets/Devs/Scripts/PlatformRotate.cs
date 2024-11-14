using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    [SerializeField] private float speed;
    private float rotation = 0;
    private void Update()
    {
        rotation = speed * Time.deltaTime * 10;
        if (rotation > 360)
        {
            rotation = 0;
        }
        transform.Rotate(0, rotation, 0);
    }
}
