using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    [Header("Attack 1")]
    public int attack1Damage = 40;
    public float attack1Rate = 2f;
    public float attack1Range = 0.5f;

    [Header("Attack 2")]
    public int attack2Damage = 60;
    public float attack2Rate = 3f;
    public float attack2Range = 0.7f;

    [Header("Attack 3")]
    public int attack3Damage = 80;
    public float attack3Rate = 4f;
    public float attack3Range = 0.8f;

    private float nextAttackTime = 0f;
    private CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            bool isRunning = characterMovement.IsRunning();
            bool isJumping = !characterMovement.IsGrounded();

            if (!isRunning && !isJumping)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Attack(1);
                    nextAttackTime = Time.time + 1f / attack1Rate;
                }
                else if (Input.GetKeyDown(KeyCode.H))
                {
                    Attack(2);
                    nextAttackTime = Time.time + 1f / attack2Rate;
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    Attack(3);
                    nextAttackTime = Time.time + 1f / attack3Rate;
                }
            }
        }
    }

    void Attack(int attackType)
    {
        float attackRange = 0f;
        int attackDamage = 0;

        if (attackType == 1)
        {
            animator.SetTrigger("Attack1");
            attackRange = attack1Range;
            attackDamage = attack1Damage;
        }
        else if (attackType == 2)
        {
            animator.SetTrigger("Attack2");
            attackRange = attack2Range;
            attackDamage = attack2Damage;
        }
        else if (attackType == 3)
        {
            animator.SetTrigger("Attack3");
            attackRange = attack3Range;
            attackDamage = attack3Damage;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attack1Range); // Saldırı 1 için range kullanılıyor
        Gizmos.DrawWireSphere(attackPoint.position, attack2Range); // Saldırı 2 için range kullanılıyor
        Gizmos.DrawWireSphere(attackPoint.position, attack3Range); // Saldırı 3 için range kullanılıyor
    }
}
