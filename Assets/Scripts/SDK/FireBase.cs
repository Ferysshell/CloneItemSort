using Firebase.Extensions;
using UnityEngine;

public class FireBase : MonoBehaviour
{
    private void Awake()
    {
        InitializeFirebase();
    }



private void InitializeFirebase()
{
    Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            // Initialize Firebase
            Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
        

            // Subscribe to token and message received events
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

            Debug.Log("Firebase is ready to use.");
        }
        else
        {
            Debug.LogError(System.String.Format(
               "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        }
    });
}

private void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
{
    Debug.Log("Received Registration Token: " + token.Token);
    // You can log the token here to check if it is received
    Debug.Log("Token: " + token.Token);
}

private void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
{
    Debug.Log("Received a new message from: " + e.Message.From);
}
}
