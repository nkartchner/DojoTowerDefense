using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    private Enemy targetEnemy;
    public List<Enemy> targetEnemies;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 4f;
    public float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int lDoT = 30;
    public float slowAmount = .5f;

    public LineRenderer lLineRend;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Flamethrower")]
    public bool flameThrower = false;

    public int ftDoT = 50;
    public LineRenderer ftLineRend;
    public ParticleSystem flameEffect;

    [Header("Crowd Control")]
    public bool crowdControl = false;

    public float allSlow = 0.5f;
    public ParticleSystem slowEffect;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

	[Header("Upgraded Turrent FirePoints")]
	public Transform firePoint;
	public Transform firePoint2;
	public Transform firePoint3;

    public Transform partToRotate;
	public float turnSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        if (crowdControl)
            slowEffect.Stop();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        targetEnemies.Clear();

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (crowdControl)
            {
                if (distanceToEnemy <= range)
                {
                    targetEnemy = enemy.transform.GetComponent<Enemy>();
                    targetEnemies.Add(targetEnemy);
                    slowEffect.Play();
                }
            }
            else
            {
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
                if (nearestEnemy != null && shortestDistance <= range)
                {
                    target = nearestEnemy.transform;
                    targetEnemy = nearestEnemy.GetComponent<Enemy>();
                }
                else
                {
                    target = null;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (crowdControl)
        {
            if (targetEnemies.Count > 0)
            {
                foreach(Enemy enemy in targetEnemies)
                {
                    enemy.Slow(allSlow);
                }
                return;
            }
            slowEffect.Stop();
        }

        if (target == null)
        {
            if (useLaser)
            {
                if (lLineRend.enabled)
                {
                    lLineRend.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            if (flameThrower)
            {
                if (ftLineRend.enabled)
                {
                    ftLineRend.enabled = false;
                    flameEffect.Stop();
                }
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else if (flameThrower)
        {
            FlameThrower();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void FlameThrower ()
    {
        targetEnemy.TakeDamage(ftDoT * Time.deltaTime);
        
        if (!ftLineRend.enabled)
        {
            ftLineRend.enabled = true;
            flameEffect.Play();
        }

        ftLineRend.SetPosition(0, firePoint.position);
        ftLineRend.SetPosition(1, target.position);

        Vector3 dir = target.position - firePoint.position;

        flameEffect.transform.position = firePoint.position + dir.normalized;

        flameEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Laser ()
    {
        targetEnemy.TakeDamage(lDoT * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lLineRend.enabled)
        {
            lLineRend.enabled = true;
            impactEffect.Play();

            impactLight.enabled = true;
        }

        lLineRend.SetPosition(0, firePoint.position);
        lLineRend.SetPosition(1, target.position);


		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

    void CrowdControl ()
    {
        foreach(Enemy enemy in targetEnemies)
        {
            enemy.Slow(allSlow);
            slowEffect.Play();
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

		Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);       
    }
}
