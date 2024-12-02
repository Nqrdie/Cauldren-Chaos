using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSink : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    public IEnumerator Sink()
    {
        Vector3 pos = platform.transform.position;
        pos.y = -0.4f;
        Vector3.Slerp(platform.transform.position, pos, 1 * Time.deltaTime);
        yield return null;
    }
}
