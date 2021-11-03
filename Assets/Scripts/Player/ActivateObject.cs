using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ActivateObject : MonoBehaviour
{
    public GameObject objectToActivate;
    public UnityEvent action = new UnityEvent();

    private void OnCollisionEnter2D(Collision2D _coll)
    {
        if (_coll.gameObject.tag == "Player")
        {
            objectToActivate.SetActive(true);
            action.Invoke();
            Destroy(gameObject);
        }
    }
}
