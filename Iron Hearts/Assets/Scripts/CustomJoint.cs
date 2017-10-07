using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HingeJoint))]
public class CustomJoint : MonoBehaviour
{

    HingeJoint _hj;

    HingeJoint joint
    {
        get
        {
            if (_hj == null) _hj = GetComponent<HingeJoint>();
            return _hj;
        }
    }

    Rigidbody target
    {
        get
        {
            return joint.connectedBody;
        }
    }

    Vector3 axis
    {
        get
        {
            return target.transform.rotation * joint.axis;
        }
    }

    Vector3 anchor
    {
        get
        {
            return target.transform.position - target.transform.rotation * joint.anchor;
        }
    }


    public float min = -90;
    public float max = 90;

    public bool relaxed;

    [Range(0, 1)]
    public float pos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var lims = joint.limits;
        if (relaxed)
        {
            lims.min = min;
            lims.max = max;
        }
        else
        {
            lims.min = Mathf.Lerp(lims.min, Mathf.Lerp(min, max, pos) - .5f, .05f);
            lims.max = Mathf.Lerp(lims.max, Mathf.Lerp(min, max, pos) + .5f, .05f);
        }
        joint.limits = lims;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.red, Color.yellow, .5f);
        Gizmos.DrawRay(anchor - axis, axis*2);
        Gizmos.color = Color.white;
        DrawArc();
    }


    void DrawArc()
    {
        Gizmos.DrawRay(anchor, Quaternion.AngleAxis(min, axis) * -target.transform.up);
        Gizmos.DrawRay(anchor, Quaternion.AngleAxis(max, axis) * -target.transform.up);
        for (int i = 0; i < 10; i++)
        {
            float curr = Mathf.Lerp(max, min, i * 1f / 10);
            float next = Mathf.Lerp(max, min, (i + 1) * 1f / 10);
            Gizmos.DrawLine(anchor + Quaternion.AngleAxis(curr, axis) * -target.transform.up,
                            anchor + Quaternion.AngleAxis(next, axis) * -target.transform.up);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawRay(anchor, Quaternion.AngleAxis(Mathf.Lerp(joint.limits.min, joint.limits.max, pos), axis) * -target.transform.up);
        Gizmos.color = Color.white;
    }


}
