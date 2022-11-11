using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    protected Vector3 vector;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public Animator animator;

    // protected void Move(string _dir) {
    //     StartCoroutine(MoveCoroutine(_dir));
    // }

    // IEnumerator MoveCoroutine(string _dir) {
        
    // }
}
