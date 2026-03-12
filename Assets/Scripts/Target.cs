using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 靶子
/// </summary>
public class Target : MonoBehaviour
{
    public float radius = 1; //半径
    public Transform center; //中心点
   
    /// <summary>
    /// 被击中某个点
    /// </summary>
    /// <param name="point"></param>
    public void OnHitPoint(Vector3 point)
    {
        point = center.InverseTransformPoint(point); //从世界坐标转换为本地坐标
        float dis = Mathf.Sqrt(Mathf.Pow(point.x, 2) + Mathf.Pow(point.y, 2)) * transform.localScale.x; //计算距离圆心的距离 使用勾股定理
        if (dis <= radius) //判断是否脱靶
        {
            int score = 10 - (int)(dis / radius * 10); //计算分数
            GameManager.Instance.score += score; //加分
        }

        gameObject.SetActive(false); //隐藏靶
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(center.position, radius);
    //}
}
