using UnityEngine;
using UnityEngine.Events;

public class DailyController : MonoBehaviour
{
    public UnityAction OnDailyCheck;
    public UnityAction OnDailyShortCheck;
    public bool IsTest;
    
    private const string key = "DailySave";
    private const string value = "checked";
    
    public void StartDailyCheck()
    {
        DailyCheck();
    }

    private void DailyCheck()
    {
        var newValue = PlayerPrefs.GetString(key);
        if (newValue != value || IsTest)
        {
            OnDailyCheck?.Invoke();
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        else
        {
            OnDailyShortCheck?.Invoke();
        }
    }
}
