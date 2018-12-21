using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{



	public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

	public GameManager gameManager;


	public Button NextWave;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    void Update ()
    {
		if (EnemiesAlive > 0) return;


		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}


		if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
			return;
        }

        countdown -= Time.deltaTime;

		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);


		waveCountdownText.text = string.Format("Next Wave in: " + "{0:00.00}", countdown);
    }

	//IEnumerator SpawnWave()
	//{
	//	float timeBetweenEnemy = 5f / waveNumber;
	//	EnemyCountUI.numOfEnemies = waveNumber * 5;
	//	PlayerStats.Rounds++;
	//	timeBetweenWaves = EnemyCountUI.numOfEnemies * timeBetweenEnemy;
	//	for (int i = 0; i < EnemyCountUI.numOfEnemies; i++)
	//	{
	//		SpawnEnemy();
	//		yield return new WaitForSeconds(0.5f);
	//	}

	//	waveNumber++;
	//}


	IEnumerator SpawnWave()
	{
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
	}


	void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
