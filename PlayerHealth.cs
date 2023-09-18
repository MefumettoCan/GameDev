using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator; // Oyuncu animasyonları

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;

        // Hasar alınca canlılığa göre animasyon oynat
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    void Die()
    {
        Debug.Log("Player Died");

        // Ölme animasyonunu oynat
        animator.SetBool("IsDead", true);

        // Gerekirse burada oyunun sonunu işle
        // Örneğin: Game Over ekranı, sahneyi yeniden yükleme vb.
    }
}
