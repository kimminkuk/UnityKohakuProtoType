using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Text talkText;

    public DBManager dbManager;
    public DataBridge dataBridge;
    public PlayerId playerId;

    public void Action(GameObject scanObj) {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        if (objData.objId <= 4000) {
            Talk(objData.objId, objData.isNpc);
        }
        DbReaction(objData.objId, objData.isNpc);
        talkPanel.SetActive(isAction);
    }

    private void Awake() {
        talkPanel.SetActive(isAction);
    }


    void Talk(int talkId, bool isNpc) {
        
        string talkData = talkManager.GetTalk(talkId, talkIndex);
        
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            return;
        }

        string[] talkDataSplit = talkData.Split(':');

        if (isNpc) {
            talkText.text = talkDataSplit[0];
        } else {
            talkText.text = talkDataSplit[0];
        }

        isAction = true;
        talkIndex++;
    }    
    public void GetTableNamesVer2() {
        string Server = "localhost";
        int Port = 8082;
        string databaseName = "test";
        string userName = "sa";
        // DBManager dbConnection = new DBManager(Server, Port, databaseName, userName);
        // List<string> tableNames = dbConnection.GetTableNames();
        // foreach (string tableName in tableNames) {
        //     Debug.Log(tableName);
        // }
    }

    void DbReaction(int objId, bool isNpc) {
        if (isNpc) {
            Debug.Log("DB Connect Start");
            switch (objId) {
                case (int)eNpc.NPC_PROG:
                    dbManager.Login("testMK@gmail.com", "testPassword");
                    break;
                case (int)eNpc.NPC_DUDE:
                    dbManager.RegisterUser("testMK@gmail.com", "testPassword");
                    //dbManager.RegisterUser("", "");
                    break;
                case (int)eNpc.NPC_PINKMAN:
                    dbManager.Logout();
                    break;
                case (int)eNpc.NPC_ANONYMOUS:
                    dbManager.LoginAnonymously();
                    break;
                case (int)eNpc.NPC_SAVE:
                    dataBridge.SaveData(playerId.Username, playerId.Password);
                    break;
                case (int)eNpc.NPC_LOAD:
                    dataBridge.LoadData(playerId.Username, playerId.Password);
                    break;
            }
            Debug.Log("DB Connect End");
        }
    }
}
