using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class BodyPart : MonoBehaviour
{

    BoxCollider _bc;

    BoxCollider coll
    {
        get
        {
            if (_bc == null) _bc = GetComponent<BoxCollider>();
            return _bc;
        }
    }

    public Transform model;

    // Update is called once per frame
    void Update()
    {
        if (model != null) model.transform.localScale = coll.size;
    }
}
