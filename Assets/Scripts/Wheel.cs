using System;
using System.Collections;
using System.Globalization;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Button spinButton;
    private DateTime lastSpin;
    public Transform wheel;
    public float timeToWait;
    public TextMeshProUGUI wonText;

    //wheel spinning
    private int randomvalue;
    private float timeInterval;
    private int finalAngle;

    private int wonKeys = 0;
    private float totalAngle;
    public int section;
    public int[] prizeValues;

    private void Start()
    {
        totalAngle = 360 / section;

        if(PlayerPrefs.HasKey("LastSpin"))
        {
            lastSpin = DateTime.Parse(PlayerPrefs.GetString("LastSpin"));
        }

        if (!IsSpinReady())
            spinButton.interactable = false;
    }

    private void Update()
    {
        if (!spinButton.IsInteractable())
        {
            if(IsSpinReady())
            {
                spinButton.interactable = true;
                timeText.text = $"00:00:00";
                return;
            }

            TimeSpan diff = DateTime.Now - lastSpin;
            double secondLeft = timeToWait - diff.TotalSeconds;

            string time = "";

            string h = ((int)secondLeft / 3600).ToString("00");
            secondLeft -= ((int)secondLeft / 3600) * 3600;
            string m = ((int)secondLeft / 60).ToString("00");
            string s = (secondLeft % 60).ToString("00");
            time = h + ":" + m + ":" + s;
            timeText.text = time;
        }
    }

    private bool IsSpinReady()
    {
        TimeSpan diff = DateTime.Now - lastSpin;
        double secondLeft = timeToWait - diff.TotalSeconds;
        
        if (secondLeft < 0)
            return true;
        
        return false;
    }

    public void StartSpin()
    {
        if (IsSpinReady())
            StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        spinButton.interactable = false;
        randomvalue = UnityEngine.Random.Range(200, 300);
        timeInterval = 0.0001f * Time.deltaTime * 2;

        for (int i = 0; i < randomvalue; i++)
        {
            wheel.transform.Rotate(0, 0, totalAngle/2);

            if (i > Mathf.RoundToInt(randomvalue * 0.2f))
                timeInterval = 0.5f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomvalue * 0.5f))
                timeInterval = 1f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomvalue * 0.7f))
                timeInterval = 1.5f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomvalue * 0.8f))
                timeInterval = 2f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomvalue * 0.9f))
                timeInterval = 2.5f * Time.deltaTime;

            yield return new WaitForSeconds(timeInterval);
        }

        if (Mathf.RoundToInt(wheel.transform.localEulerAngles.z) % totalAngle != 0)
            wheel.transform.Rotate(0, 0, totalAngle/2);

        finalAngle = Mathf.RoundToInt(wheel.transform.localEulerAngles.z);

        for(int i = 0; i< section; i++)
        {
            if (finalAngle == i * totalAngle)
                wonKeys = prizeValues[i];
        }
        //Debug.Log(wonKeys);
        Wallet.SetAmount(Wallet.keys + wonKeys);

        if (wonKeys > 1)
            wonText.text = $"You won {wonKeys} keys.";
        else
            wonText.text = $"You won {wonKeys} key.";

        wonText.gameObject.GetComponent<Animator>().Play("Show");

        lastSpin = DateTime.Now;
        PlayerPrefs.SetString("LastSpin", lastSpin.ToString());
        spinButton.interactable = false;
    }

    public static DateTime GetNetTime()
    {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        return DateTime.ParseExact(todaysDates,
        "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
        CultureInfo.InvariantCulture.DateTimeFormat,
        DateTimeStyles.AssumeUniversal);
    }
}