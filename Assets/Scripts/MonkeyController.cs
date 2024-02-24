using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MonkeyController : MonoBehaviour
{
    public static MonkeyController instance;
    public Animator animator;
    public Rigidbody rb;
    public Transform startPosition;
    public bool isAlive;
    public float moveSpeed;
    private float horMoveSpeed;
    private float tmovespeed;
    public Joystick joystick;
    private bool isBlinking;
    private SkinnedMeshRenderer mesh;
    private float tttttempp;
    public Button jumpButton;
    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Shield"))
        {
            PlayerPrefs.SetInt("Shield", 0);
            PlayerPrefs.Save();
        }
        animator = GetComponent<Animator>();
        GameManager.onGameStarted += StartMove;
        if (rb==null)
        {
            rb = GetComponent<Rigidbody>();
        }
        tmovespeed = moveSpeed;
        horMoveSpeed = moveSpeed;
    }
    private void OnDisable()
    {
        GameManager.onGameStarted -= StartMove;
    }
    private void Move()
    {
        if (transform.position.x < 12.8f && transform.position.x > 8.5f)
        {
            transform.position = new Vector3(transform.position.x - joystick.Horizontal*horMoveSpeed*1.5f*Time.deltaTime, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
            transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z - joystick.Horizontal * 15f);
        }
        else if (transform.position.x > 12.8f)
        {
            transform.position = new Vector3(transform.position.x-0.1f* moveSpeed * 2 * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 0.1f* moveSpeed * 2 * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
    public void Jump()
    {
        if (isAlive)
        {
            jumpButton.interactable = false;
            isAlive = false;
            animator.SetTrigger("isJumping");
            StartCoroutine(WaitForJmp());
        }
    }
    private IEnumerator WaitForJmp()
    {
        yield return new WaitForSeconds(1);
        transform.DOMove(transform.position+ transform.up * 3, 0.5f).OnComplete(EndJump);
    }
    private void EndJump()
    {
        isAlive = true;
        jumpButton.interactable = true;
    }
    public void StopMove()
    {
        isAlive = !isAlive;
        if (isAlive)
            animator.StopPlayback();
        else
            animator.StartPlayback();
    }
    private void StartMove()
    {
        transform.position = startPosition.position;
        transform.Rotate(new Vector3(0, 90, 0));
        animator.SetBool("isMoving", true);
        isAlive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Stop")
        {
            moveSpeed = 0;
            //animator.StopPlayback();
        }
        if (other.tag=="Obstacle" && PlayerPrefs.GetInt("Shield")==0)
        {
            StartFalling();
        }
        else if (other.tag == "Obstacle")
        {
            DestroyShield();
            Debug.Log("DestroyShield");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Stop")
        {
            moveSpeed = tmovespeed;
            //animator.StartPlayback();
        }
    }
    private void DestroyShield()
    {
        StartCoroutine(DetroyShield());
    }
    private void StartFalling()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isFalling", true);
        isAlive = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        StartCoroutine(GameManager.instance.ShowLosePanel());
    }
    private void Update()
    {
        if (GameManager.instance.IsGameStarted() && isAlive)
        {
            Move();
        }
    }
    private void FixedUpdate()
    {
        if (isBlinking)
        {
            tttttempp += Time.fixedDeltaTime;
            if (tttttempp>=0.25f)
            {
                GameManager.instance.shield.SetActive(!GameManager.instance.shield.activeSelf);
                tttttempp = 0f;
            }
        }
    }
    private IEnumerator DetroyShield()
    {
        isBlinking = true;
        yield return new WaitForSeconds(2);
        isBlinking = false;
        GameManager.instance.shield.SetActive(false);
        PlayerPrefs.SetInt("Shield", 0);
        PlayerPrefs.Save();
        Debug.Log("Shield_Destroyed");
    }
}
