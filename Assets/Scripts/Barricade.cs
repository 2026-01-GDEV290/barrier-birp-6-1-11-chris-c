using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] CollisionDetector detector;
    public GameObject Camera;
    public ScreenShaker ScreenShake;
    public GameObject boards1;
    public GameObject boards2;
    public GameObject Bomb;
    private int CollIncrementor = 0;
    public float duration = 2f; // The time it takes to scale up
    public float targetScale = 2f; // The final scale multiplier
    

    
    public void DisableAllRigidbodies()
    {
        // Get all Rigidbody components in the parent and all children
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            // Set isKinematic to true to disable physics
            rb.isKinematic = true; 
        }
    }

    // Call this method to re-enable physics
    public void EnableAllRigidbodies()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            // Set isKinematic to false to enable physics
            rb.isKinematic = false;
        }
    }


    public void Oncollision()
    {
        if (CollIncrementor == 0)
        {
            firsthit();
        }
        else if (CollIncrementor == 1)
        {
            secondhit();
        }
        else if (CollIncrementor == 2)
        {
            thridhit();
        }
        else
        {
            Debug.Log("Hits = " + CollIncrementor);
        }
    }

    public void firsthit()
    {
        if (CollIncrementor == 0){
        CollIncrementor = CollIncrementor + 1;
        Debug.Log("One Hit!");
        Rigidbody[] rigidbodies = boards1.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        StartScaling();

        ScreenShake.StartShake();
        }
    }

    public void secondhit()
    {
        CollIncrementor ++;
        Debug.Log("Two Hits!");
        Rigidbody[] rigidbodies = boards2.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        
        StartScaling();
        ScreenShake.StartShake();
    }

    public void thridhit()
    {
        Debug.Log("Three Hits!");
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
        StartScaling();
        ScreenShake.StartShake2();
        BoxCollider[] boxcolliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider bc in boxcolliders)
        {
            bc.enabled = false;
        }

    }

    void Start()
    {
        DisableAllRigidbodies();
        ScreenShake = Camera.GetComponentInChildren<ScreenShaker>();
    }

    void OnEnable()
    {
        detector.Ontargethit += Oncollision;
    }

    void OnDisable()
    {
        detector.Ontargethit -= Oncollision;
    }

    public void StartScaling()
    {
        StartCoroutine(ScaleOverTime(duration, targetScale));
    }

    private IEnumerator ScaleOverTime(float duration, float scale)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.one * scale; 
        float elapsed = 0f;

        while (elapsed < duration)
        {

            float t = elapsed / duration;


            Bomb.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;

        }

        
        Bomb.transform.localScale = startScale; 

}
}
