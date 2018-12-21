using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;
    public Color HighLives;
    public Color ThirdLives;
    public Color HalfLives;
    public Color LowLives;




    void Update()
    {
		livesText.text = $"{PlayerStats.Lives.ToString()}";
        //Debug.Log(livesText.text = $"{PlayerStats.Lives.ToString()} Lives");

        if (PlayerStats.Lives < (PlayerStats.Lives / 3)) livesText.color = LowLives;
        else if (PlayerStats.Lives < (PlayerStats.Lives / 2)) livesText.color = HalfLives;
        else if (PlayerStats.Lives < Mathf.Floor(PlayerStats.Lives / (float)1.3)) livesText.color = ThirdLives;
        else if (PlayerStats.Lives <= Mathf.Floor(PlayerStats.Lives)) livesText.color = HighLives;
    }
}
