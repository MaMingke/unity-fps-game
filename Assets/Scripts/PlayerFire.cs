using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public PlayerAKMWeapon akWeapon;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !Cursor.visible) //如果点击鼠标左键，且鼠标是隐藏的
        {
            akWeapon.Fire();
        }
    }
}
