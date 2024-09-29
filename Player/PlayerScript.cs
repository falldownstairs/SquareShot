using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 forceToApply;
    public Vector2 PlayerInput;
    public float forceDamping;
    private bool canDash = true;
    private bool isDashing = false;
    private bool canGenerateAfterImage = true;
    [SerializeField] public float moveSpeed;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;


    void Update()
    {
        if (isDashing)
        {
            if (canGenerateAfterImage){
            StartCoroutine(DashAfterImage());
            }
            return;
        }
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if((canDash == true) && Input.GetButtonDown("Jump") == true)
        {
            StartCoroutine(Dash());
        }
        
    
    }
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Vector2 moveForce = PlayerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;
    }
 

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = PlayerInput * dashPower;
        FindAnyObjectByType<AudioManager>().Play("dash");
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
    private IEnumerator DashAfterImage()
    {
        canGenerateAfterImage = false;
        PlayerAfterImagePool.instance.GetFromPool();
        yield return new WaitForSeconds(0.015f);
        canGenerateAfterImage = true;
    }
}