using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ActivateObject : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public UnityEvent action = new UnityEvent();

    private void OnCollisionEnter2D(Collision2D _coll)
    {
        if (_coll.gameObject.tag == "Player")
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
