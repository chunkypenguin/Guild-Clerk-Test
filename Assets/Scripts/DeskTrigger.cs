using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    public List<GameObject> items;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //Debug.Log(other.gameObject.tag.ToString());
            //items.Add(other.gameObject);
            AddObject(other.gameObject);
        }
    }

    public void AddObject(GameObject obj)
    {
        if (!items.Contains(obj))
        {
            items.Add(obj);
            Debug.Log(obj.name + " added to the list.");
        }
    }
}
