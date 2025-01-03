using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    public float speed;
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
