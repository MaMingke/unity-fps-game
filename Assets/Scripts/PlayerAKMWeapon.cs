using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAKMWeapon : MonoBehaviour
{
    public int damage = 1; //伤害
    public float fireRate = 10; //射击速度
    public AudioClip shootClip; //射击音效
    public Transform gunCamera; //枪的Camera
    public Bullet bulletPrefab; //子弹预制体
    public Transform bulletGeneratePoint; //子弹生成点
    public ParticleSystem muzzleflash; //枪口火焰

    private Animator animator; //动画控制器组件
    private float lastFire = 0; //开火计时器
    private bool canShoot = false; //是否可以射击
    private AudioSource weaponAudio;
    private Ray ray; //射线
    private RaycastHit raycastHit; //射线碰撞信息


    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponAudio = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Time.time - lastFire >= 1 / fireRate)
        {
            canShoot = true;
      
        }
    }

    public void Fire()
    {
        if (!canShoot) return;
        canShoot = false;
        lastFire = Time.time; //记录射击时间点
        animator.Play("Fire", 0, 0.1f); //播放射击动画
        muzzleflash.Emit(1); //播放一次枪口火焰粒子特效
        weaponAudio.PlayOneShot(shootClip); //播放射击音效
        ray = new Ray(gunCamera.position, gunCamera.forward); //从镜头到镜头正前方发出射线
        Vector3 dir = Vector3.zero; //计算子弹运动方向
        if (Physics.Raycast(ray, out raycastHit, 100))  //从镜头到前方100米范围内射线碰撞检测
        {
            //如果碰撞到了碰撞体，子弹运动方向为从子弹生成点到碰撞点
            dir = (raycastHit.point - bulletGeneratePoint.position).normalized;
        }
        else
        {
            //如果未碰撞到碰撞体，子弹运动方向为从子弹生成点到射线20米处
            dir = (ray.GetPoint(20) - bulletGeneratePoint.position).normalized;
        }
        //在子弹生成点处生成子弹
        Bullet bullet = Instantiate(bulletPrefab, bulletGeneratePoint.position, bulletGeneratePoint.rotation);
        //赋予伤害值
        bullet.damage = damage;
        //设置子弹刚体运动速度和方向
        bullet.Rigid.velocity = dir * 50;
    }
}
