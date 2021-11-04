using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ActivateObject : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public UnityEvent action = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (_coll.CompareTag("Player"))
        {
            for (int i = 0; i < objectsToActivate.Count; i++)
            {
                objectsToActivate[i].SetActive(true);
            }
            action.Invoke();
            Destroy(gameObject);
        }
    }
}
