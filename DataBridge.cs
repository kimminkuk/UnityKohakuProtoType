using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class DataBridge : MonoBehaviour
{
    PlayerId data;
    string DATA_URL = "https://kohaku-be3d4-default-rtdb.firebaseio.com/";
    string userID;

    DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData(string usernameInput, string passwordInput)
    {
        if (!usernameInput.Equals("") && passwordInput.Equals(""))
        {
            Debug.Log("NO DATA");
            return;
        }

        data = new PlayerId(usernameInput, passwordInput);
        string jsonData = JsonUtility.ToJson(data);

        
        //c# random number code
        

        databaseReference.Child("Users" + Random.Range(0, 1000000)).SetRawJsonValueAsync(jsonData);
        //databaseReference.Child("Users").SetRawJsonValueAsync(jsonData);
    }
    public void LoadData(string usernameInput, string passwordInput)
    {

        //FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync()
        //    .ContinueWith(task => {
        //        if (task.IsCanceled) {
        //            Debug.Log("IsCanceled");
        //        }
        //        if (task.IsFaulted) {
        //            Debug.Log("Error");
        //        }
        //        else if (task.IsCompleted)
        //        {
        //            Debug.Log("[1] Login Failed");
        //            DataSnapshot snapshot = task.Result;
        //            string playerData = snapshot.GetRawJsonValue();
        //            Debug.Log("Data is: " + playerData);
        //            Debug.Log("[2] Login Failed");
        //        }
        //});
        Debug.Log("[1] LoadData Call()");
        databaseReference.Child("Users").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve data from Firebase database.");
            }
            else if (task.IsCompleted)
            {
                // Parse the data to a C# object
                DataSnapshot snapshot = task.Result;
                string playerData = snapshot.GetRawJsonValue();
                Debug.Log("Data is: " + playerData);

                //Dictionary<string, object> usersData = (Dictionary<string, object>)snapshot.Value;
                //List<PlayerId> usersList = new List<PlayerId>();
                //foreach (var userData in usersData)
                //{
                //    Dictionary<string, object> user = (Dictionary<string, object>)userData.Value;
                //    PlayerId newUser = new PlayerId
                //    {
                //        Username = (string)user["Username"],
                //        Password = (string)user["Password"]
                //    };
                //    usersList.Add(newUser);
                //}

                //// Do something with the data
                //foreach (PlayerId user in usersList)
                //{
                //    Debug.Log("Username: " + user.Username + " | Password: " + user.Password);
                //}
            }
        });
        Debug.Log("[2] LoadData Call()");
    }
}
