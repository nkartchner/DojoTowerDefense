using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float startSpeed = 10f;
    public float startHealth = 100f;

    [HideInInspector]
    public float speed;
    public bool slowed;

    private float health;

    public int worth = 5;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

	private bool isDead = false;

    void Start()
	{
        speed = startSpeed;
        health = startHealth;
        slowed = false;
    }

    void Update()
    {
        if (slowed)
            Slow(0.5f);
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die ()
    {
		isDead = true;

        PlayerStats.Money += worth;

		GameObject effectIns = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
    }
}
