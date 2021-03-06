using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add this script to any rigidbody which is a child of another rigidbody and needs to move with the parent
//by default rigidbodies override the transform and do not follow their parent

public class RigidBodyMoveWithParent : MonoBehaviour
{
	Vector2 relativePos;
	Rigidbody2D rigid;
	void Start()
	{
		relativePos = transform.localPosition;
		rigid=GetComponent<Rigidbody2D>();
	}
        private void FixedUpdate() {
        Vector3 parentPos;

        if (transform.parent.GetComponent<Rigidbody2D>()) parentPos = transform.parent.GetComponent<Rigidbody2D>().position;
        else parentPos = transform.parent.transform.position;    
        rigid.position = parentPos;
           
           
        }

}
