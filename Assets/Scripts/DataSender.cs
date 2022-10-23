using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using System;
using Firebase.Database;

public class DataSender : MonoBehaviour 
{
    private DatabaseReference refarence;
    private int id;

    // Start is called before the first frame update
    public void Start()
    {
        id = new System.Random().Next();
        refarence = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void SendData(string userName, int count, TensityData data)
    {
        string json = JsonUtility.ToJson(data);

        refarence.Child("TensityData").Child(userName).Child(id.ToString()).Child(count.ToString()).SetRawJsonValueAsync(json);
        //refarence.Child("name").SetValueAsync("aaa");
    }

    public void PushData(TensityData data)
    {
        string json = JsonUtility.ToJson(data);

        var key = refarence.Push().Key;

    }
}
