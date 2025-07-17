using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class APIrequest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    IEnumerator GetJoke() // Chuck Norris Joke API request
    {
        string url = "https://api.chucknorris.io/jokes/random";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Parse JSON into String
            string rawJson = request.downloadHandler.text;
            string joke = JsonUtility.FromJson<JokeResponse>(rawJson).value;
            Debug.Log(joke);
        }
        else
        {
            Debug.Log("Error fetching joke" + request.error);
        }
    }
    [System.Serializable]
    public class  JokeResponse
    {
        public string value;
    }
}
