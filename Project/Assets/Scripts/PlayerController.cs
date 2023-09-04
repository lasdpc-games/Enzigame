/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float angleZ;
    public float moveSpeed, rotationSpeed;

    public Joystick moveJoystick, rotateJoystick;

    SpriteRenderer spriteRenderer;
    public Sprite[] playerSprites;
    public int type = 0;
    public int helperType = 0;
    public int level;
    public static bool isMobile = false;

    //PC:
    int rotateInput;
    int maxSpeed = 12;

    void Start(){

        if(isMobile){
            moveJoystick.gameObject.SetActive(true);
            rotateJoystick.gameObject.SetActive(true);
        }else{
            moveJoystick.gameObject.SetActive(false);
            rotateJoystick.gameObject.SetActive(false);
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

    }

    void Update(){
        Move();
        if (Input.GetKeyDown("3")){
            ChangePlayerViaButton(0);
        }else if (Input.GetKeyDown("2")){
            ChangePlayerViaButton(2);
        }else if (Input.GetKeyDown("1")){
            ChangePlayerViaButton(4);
        }
    }

    void Move(){
            if(moveJoystick.transform.GetChild(0).GetComponent<RectTransform>().localPosition != Vector3.zero){
                rb.AddForce(new Vector2(moveJoystick.Horizontal*moveSpeed*Time.deltaTime, moveJoystick.Vertical*moveSpeed*Time.deltaTime));
            }
            if(rotateJoystick.transform.GetChild(0).GetComponent<RectTransform>().localPosition != Vector3.zero){
                this.transform.GetChild(0).Rotate(0, 0, rotateJoystick.Horizontal*rotationSpeed*Time.deltaTime * -1);
            }
    
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime));
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            this.transform.GetChild(0).Rotate(0, 0, Input.GetAxis("RotateJL")*rotationSpeed * Time.deltaTime * -1);

            angleZ = Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),transform.rotation);
    }

    public void ChangePlayerViaButton(int helper){
        if(type >= 0 ){
            if(level > 1){
                ChangePlayer(helper);
            }
        }
    }
    public void ChangePlayer(int helper){
        if (helper == 0){
            spriteRenderer.sprite = playerSprites[0];
            type = 0;
        }else if (helper == 2){
            spriteRenderer.sprite = playerSprites[1];
            type = 1;
        }else if (helper == 4){
            spriteRenderer.sprite = playerSprites[2];
            type = 2;
        }else if (helper == -1){
            spriteRenderer.sprite = playerSprites[type + 3];
            helperType = type;
            type = -1;
        }else if(helper == -2){
            type = helperType;
            spriteRenderer.sprite = playerSprites[type];
        }
    }

    void OnCollision2D(Collision2D other){
        PushObjects(other);
    }

    void PushObjects(Collision2D other){
        Vector3 forceDirection = other.gameObject.transform.position - transform.position;
        forceDirection.y = 0;
        forceDirection.Normalize();
        Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
        otherRB.AddForceAtPosition(forceDirection*3,transform.position,ForceMode2D.Impulse);
    }

}
