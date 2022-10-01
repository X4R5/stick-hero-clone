using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class StickMovements
    {
        public static void Grow(Transform stickSprite, float growSpeed){
            stickSprite.transform.localScale += new Vector3(0, (growSpeed / 10) * Time.deltaTime, 0);
            stickSprite.transform.localPosition += new Vector3(0, (growSpeed / 10) * 8.45f * Time.deltaTime, 0);
        }
        public static void Rotate(GameObject stick, Quaternion targetRoatation, float rotationSpeed){
            stick.transform.rotation = Quaternion.Slerp(stick.transform.rotation, targetRoatation, rotationSpeed * Time.deltaTime);
        }
        public static void DestroyStick(GameObject stick){
            var rb = stick.GetComponent<Rigidbody2D>();
            var body = stick.transform.GetChild(0);
            body.GetComponent<SpriteRenderer>().color = Color.red;
            rb.gravityScale = 1;
        }
    }
}
