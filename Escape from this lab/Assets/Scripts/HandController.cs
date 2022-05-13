using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Vector2 _boxPos;
    private bool _collisionTrue;
    private GameObject _box;
    private bool _boxTaked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            _collisionTrue = true;
            _box = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            _collisionTrue = false;
            _box = null;
        }
    }

    public void TakeBox()
    {
        if (_collisionTrue)
        {
            if (_boxTaked == false)
            {
                _box.transform.SetParent(this.transform);
                _box.transform.localPosition = _boxPos;

                _box.GetComponent<Rigidbody2D>().isKinematic = true;
                _box.GetComponent<BoxCollider2D>().isTrigger = true;

                _boxTaked = true;
            }

            else
            {
                _box.transform.SetParent(null);

                _box.GetComponent<Rigidbody2D>().isKinematic = false;
                _box.GetComponent<BoxCollider2D>().isTrigger = false;
                _boxTaked = false;
            }
        }
    }
}
