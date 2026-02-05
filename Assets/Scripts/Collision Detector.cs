using UnityEngine;
using System;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private Gorilla gorilla;
    public event Action Ontargethit;
    public string targetTag;

    void Awake()
    {
        if (gorilla == null)
            gorilla = GetComponentInParent<Gorilla>(); // hitbox is usually a child
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            if (!gorilla.Hashit){
            Debug.Log(gameObject.name + " triggered with the specific object: " + other.gameObject.name);
            Ontargethit?.Invoke();
            gorilla.Hashit = true;
            }
        }
    }
}
