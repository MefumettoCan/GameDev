using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       
    public float jumpForce = 10f;      
    public Transform groundCheck;      
    public LayerMask groundLayer;      
    public Animator animator;  // Animator bileşeni

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool IsRunning()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        return Mathf.Abs(moveInput) > 0.1f; // Hareket girişi varsa koşuyor kabul edebiliriz
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Hareket yönüne göre karakterin görünümünü çevirme
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // Sağa dön
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Sola dön
        }

        // Animator parametrelerini güncelleme
        animator.SetFloat("Speed", Mathf.Abs(moveInput)); // Koşma animasyonu için hız parametresi
        animator.SetBool("IsJumping", !isGrounded); // Zıplama animasyonu için zıplama durumu parametresi
    }

}
