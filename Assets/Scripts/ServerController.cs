using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ServerController : MonoBehaviour
{
    public UnityAction OnServerOn;
    public UnityAction OnServerOff;
    public string ResponseResult;
    
    [SerializeField] private string API;

    private Coroutine responseCoroutine;
    private const string key = "RR";

    #region public functions

    public void GetServerResponse()
    {
        responseCoroutine ??= StartCoroutine(Response(API));
    }
    
    public void GetShortServerResponse()
    {
        ResponseResult = PlayerPrefs.GetString(key);
        if (!string.IsNullOrEmpty(ResponseResult))
        {
            OnServerOn?.Invoke();
        }
        else
        {
            OnServerOff?.Invoke();
        }
    }

    #endregion public functions
    
    #region private functions

    private IEnumerator Response(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
 
        if (request.result != UnityWebRequest.Result.Success) 
        {
            OnServerOff?.Invoke();
        }
        else 
        {
            ResponseResult = request.downloadHandler.text;
            PlayerPrefs.SetString(key, ResponseResult);
            PlayerPrefs.Save();
            OnServerOn?.Invoke();
        } 
    }

    #endregion private functions
}
