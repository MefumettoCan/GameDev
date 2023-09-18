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

    // Roll mekaniği için gerekli değişkenler
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    private bool isRolling = false;
    private float rollStartTime;
    private float airSpeedY; // Y eksenindeki hız

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
        // Roll mekaniğini başlat
        if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            StartRoll();
        }

        // Roll hareketi
        if (isRolling)
        {
            Roll();
        }
        else
        {
              isGrounded = IsGrounded();
        airSpeedY = rb.velocity.y;

        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
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
        animator.SetFloat("AirSpeedY", airSpeedY); // AirSpeedY parametresini güncelleme
        animator.SetBool("IsGrounded", IsGrounded()); // Animator parametresini güncelleme
    }
}

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void StartRoll()
    {
        isRolling = true;
        rollStartTime = Time.time;
        animator.SetTrigger("Roll"); // Roll animasyonunu başlat
    }

    private void Roll()
{
    // Karakterin yönünü kontrol et
    float moveInput = Input.GetAxisRaw("Horizontal");
    float rollDirection = (moveInput > 0) ? 1f : -1f; // Sağa veya sola doğru roll

    // Karakteri sağa veya sola doğru hızlıca kaydır
    Vector2 rollVelocity = new Vector2(rollDirection * rollSpeed, rb.velocity.y);
    rb.velocity = rollVelocity;

    // Roll süresi dolduğunda roll mekaniğini durdur
    if (Time.time - rollStartTime >= rollDuration)
    {
        isRolling = false;
        animator.ResetTrigger("Roll"); // Roll animasyonunu sıfırla
    }
}

}
