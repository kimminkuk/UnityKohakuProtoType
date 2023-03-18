using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbtiVer1 : MonoBehaviour
{
    public string Username;
    public string Password;
    public string MbtiLog;
    public MbtiVer1()
    {
        
    }

    public MbtiVer1(string name, string password, string MbtiLog)
    {
        Username = name;
        Password = password;
        MbtiLog = MbtiLog;
    }
}
