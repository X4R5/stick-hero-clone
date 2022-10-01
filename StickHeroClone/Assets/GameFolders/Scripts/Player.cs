using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class Player
    {
        public static void SetPosition(GameObject player, Vector3 targetPos, float moveSpeed){
            player.transform.position = Vector3.Lerp(player.transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
