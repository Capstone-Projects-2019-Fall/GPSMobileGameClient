using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool shaking = false;

    [SerializeField] private float ferocity;

    private void Update()
    {
        if (shaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * ferocity);
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

        }
    }

    public void ShakeIt()
    {
        StartCoroutine("ShakeNow");

    }

    IEnumerator ShakeNow()
    {
        Vector3 originalPos = transform.position;

        if (!shaking)
        {
            shaking = true;
        }

       yield return new WaitForSeconds(0.5f);

        shaking = false;
        transform.position = originalPos;

    }
}
