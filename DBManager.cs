using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;
using Firebase.Auth;

public class DBManager : MonoBehaviour
{
    public void Login(string email, string password) {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).
            ContinueWith( task => {
                if (task.IsCanceled) {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted) {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsCompleted) {
                    Debug.Log("User is LOGGED in");
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
    }

    public void Logout() {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null) {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    public void LoginAnonymously() {
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().
            ContinueWith(task => {
                if (task.IsCanceled) {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted) {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
    }

    public void RegisterUser(string email, string password) {

        if (email.Equals("") && password.Equals("")) {
            Debug.Log("Please enter email and password to register");
            return;
        }

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).
            ContinueWith(task => {
                if (task.IsCanceled) {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted) {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsCompleted) {
                    Debug.Log("Registeration COMPLETE");
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
    }

    void GetErrorMessage(AuthError errorCode) {
        string msg = "";
        msg = errorCode.ToString();
        switch (errorCode) {
            case AuthError.AccountExistsWithDifferentCredentials:
                msg = "Account already exists with different credential";
                break;
            case AuthError.MissingPassword:
                msg = "Missing password";
                break;
            case AuthError.WrongPassword:
                msg = "Wrong password";
                break;
            case AuthError.InvalidEmail:
                msg = "Invalid email";
                break;
        }
        Debug.Log(msg);
    }

    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}