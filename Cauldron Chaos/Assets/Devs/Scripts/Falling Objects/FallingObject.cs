using System.Collections;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private GameObject childObject;

    [SerializeField] private float timeToFade;
    private Color objColor;
    private Color alphaColor;
    private Material objMaterial;
    [SerializeField] float shadowSize;
    private Vector3 size;
    private Vector3 originalSize;
    private Vector3 childSize;

    bool hasCollided;

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

    private void Update()
    {
        UpdateShadow();
    }


    private void OnCollisionEnter(Collision collision)
    {
        print("Collided");
        if (!collision.gameObject.CompareTag("Player"))
        {
            hasCollided = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!hasCollided)
            {
                collision.gameObject.SetActive(false);
            }

            return;
        }

        Destroy(childObject);
        StartCoroutine(Despawn());
    }

    private void UpdateShadow()
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
