using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGround;
    public bool _itemInHands;
    public bool _stopMove;

    [SerializeField] private int _runSpeed;
    [SerializeField] private int _jumpForce;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private bool _lockRoll;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isRight = true;
    private GameObject _handsItem;

    private void Start()
    {
        //
        // Инициализация полей
        //
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PlayerMove();
        DropItem();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }

    public void ChangeMoveState()
    {
        _stopMove = !_stopMove;
    }

    public void TakeItem(GameObject child, Vector2 posInHands)
    {
        if (_itemInHands == false)
        {
            _handsItem = child;
            child.transform.parent = transform;
            child.transform.localPosition = posInHands;
            child.GetComponent<BoxCollider2D>().isTrigger = true;
            child.GetComponent<Rigidbody2D>().isKinematic = true;
            _animator.Play("PlayerItemsInHandStay");
            _itemInHands = true;
        }
    }

    public void DropItem()
    {
        if (_itemInHands == true && Input.GetKeyDown(KeyCode.Q))
        {
            _handsItem.transform.parent = null;
            _handsItem.GetComponent<BoxCollider2D>().isTrigger = false;
            _handsItem.GetComponent<Rigidbody2D>().isKinematic = false;
            _animator.Play("PlayerStay");
            _itemInHands = false;

        }
    }

    private void PlayerMove()
    {
        if (_stopMove == false)
        {
            //
            //Управление
            //
            float moveInput = Input.GetAxis("Horizontal");
            float jumpInput = Input.GetAxis("Jump");

            _rb.velocity = new Vector2(_runSpeed * moveInput, _rb.velocity.y);

            if (jumpInput > 0 && isGround == true)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce * jumpInput);
            }
            //
            //Анимации
            //
            if (_itemInHands == false)
            {
                if (moveInput != 0)
                {
                    _animator.SetBool("IsRun", true);
                }

                else
                {
                    _animator.SetBool("IsRun", false);
                }
            }

            else
            {
                if (moveInput != 0)
                {
                    _animator.SetBool("ItemRun", true);
                }

                else
                {
                    _animator.SetBool("ItemRun", false);
                }
            }
            //
            // Поворот игрока
            //
            if (moveInput < 0 && _isRight == true)
            {
                Flip();
            }

            else if (moveInput > 0 && _isRight == false)
            {
                Flip();
            }
        }

    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        _isRight = !_isRight;
    }
}
