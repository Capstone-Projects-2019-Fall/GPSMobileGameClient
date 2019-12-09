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

            transform.position = newPos;
            //Debug.Log(transform.position.ToString());

        }
    }

    public void ShakeIt()
    {
        //Debug.Log("Starting to shake");
        StartCoroutine("ShakeNow");

    }

    IEnumerator ShakeNow()
    {
        //Debug.Log("Shaking Now");
        Vector3 originalPos = transform.position;

        if (!shaking)
        {
            shaking = true;
        }

       yield return new WaitForSeconds(0.25f);

        shaking = false;
        transform.position = originalPos;

    }
}
