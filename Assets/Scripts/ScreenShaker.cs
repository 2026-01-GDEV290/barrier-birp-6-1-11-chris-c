using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour
{
    public float duration = 0.3f;
    public float duration2 = 1f;
    
    public void StartShake(){
        StartCoroutine(Shake());
    }
    IEnumerator Shake(){
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere;
            yield return null;
        }
        transform.position = startPosition;
    }

    public void StartShake2(){
    StartCoroutine(Shake2());
    }
    IEnumerator Shake2(){
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration2){
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere;
            yield return null;
        }
        transform.position = startPosition;
    }
}
