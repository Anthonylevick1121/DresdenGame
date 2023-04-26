using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TriggerEnterEvent : MonoBehaviour
{
    public UnityEvent<PlayerCore> playerEnterAction;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerEnterAction.Invoke(other.gameObject.GetComponent<PlayerCore>());
    }
}