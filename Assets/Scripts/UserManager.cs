using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserManager : Singleton<UserManager>
{
    private int points;

    [SerializeField]
    private int timer;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    public void AddPoints()
    {
        points += 100;
        HudManager.Instance.ChangePoints(points);
    }

    private IEnumerator Countdown()
    {
        while (true)
        {

            yield return new WaitForSecondsRealtime(1f);
            timer--;

            HudManager.Instance.ChangeTimer(timer);

            if (timer <= 0)
            {
                Debug.Log("stop");
                SceneManager.LoadScene(0);
                break;
            }
        }
    }
}