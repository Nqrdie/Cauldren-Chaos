using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float timeToFade;
    private Color objColor;
    private Color alphaColor;
    private Material objMaterial;
    [SerializeField] private GameObject childObject;
    [SerializeField] float shadowSize;
    private bool collided = false;

    private Vector3 size;
    private Vector3 originalSize;
    private Vector3 childSize;

    private void Start()
    {
        objMaterial = gameObject.GetComponent<Renderer>().material;
        objColor = objMaterial.color;

        alphaColor = objColor;
        alphaColor.a = 0;

        size = Vector3.zero;
        originalSize = gameObject.transform.localScale;
        childSize = gameObject.transform.localScale / 4;
}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(childObject);
        StartCoroutine(Despawn());
        collided = true;
    }
    private void Update()
    {
        if (childObject != null && size != originalSize)
        {
            size = new Vector3(
                childSize.x += shadowSize * Time.deltaTime, 
                childSize.y += shadowSize * Time.deltaTime, 
                childSize.z += shadowSize * Time.deltaTime
                );

            childObject.transform.localScale = size;
        }

        if (size == originalSize) { print("yes"); }
    }
    private IEnumerator Despawn()
    {
        float elapsedTime = 0f;
       
        while (elapsedTime < timeToFade)
        {
            objMaterial.color = Color.Lerp(objColor, alphaColor, elapsedTime / timeToFade);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objMaterial.color = alphaColor;

        Destroy(gameObject);
    }
}
