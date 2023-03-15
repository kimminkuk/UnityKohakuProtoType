using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public float speed = 2f;
    GameObject scanObject;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update() {
        if (Input.GetButtonDown("Jump") && scanObject != null) {
            gameManager.Action(scanObject);
        }
    }

    private void FixedUpdate() {
        //Player의 이동
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(h, v);
        rigid.velocity = moveDir.normalized * speed;
        //transform.Translate(moveDir.normalized * speed * Time.deltaTime);

        Debug.DrawRay(rigid.position, moveDir, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, moveDir, 1f, LayerMask.GetMask("Object"));
        if (rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        } else {
            scanObject = null;
        }

        // //Ray를 쏘아 NPC와 상호작용 합니다.
        // if(Input.GetMouseButtonDown("Jump")){
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hitInfo;

        //     if(Physics.Raycast(ray, out hitInfo)){
        //         GameObject hitObject = hitInfo.collider.gameObject;
        //         NPC npc = hitObject.GetComponent<NPC>();
        //         if(npc != null){
        //             npc.OnInteract();
        //         }
        //     }
        // }
    }
}
