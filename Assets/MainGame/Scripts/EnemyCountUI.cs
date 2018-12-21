using UnityEngine;
using UnityEngine.UI;

public class EnemyCountUI : MonoBehaviour
{
    public static int numOfEnemies;
    public Text EnemiesRemainingText;

    void Update()
    {
        EnemiesRemainingText.text = $"{WaveSpawner.EnemiesAlive} Enemies Left.";
	}
}
