using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerId : MonoBehaviour
{
    [SerializeField]
    public string Username;
    public string Password;

    public PlayerId()
    {

    }

    public PlayerId(string name, string password)
    {
        Username = name;
        Password = password;
    }
}
