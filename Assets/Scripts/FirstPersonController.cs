using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    [Header("视角旋转")]
    [SerializeField] private Transform headTransform; //头部
    [SerializeField] private float xSensitivity = 2f; //横向灵敏度
    [SerializeField] private float ySensitivity = 2f; //纵向灵敏度
    [SerializeField] private bool clampVerticalRotation = true; //是否限制纵向旋转
    [SerializeField] private float minimumX = -90f; //最小角度
    [SerializeField] private float maximumX = 90f; //最大角度
    [SerializeField] private bool smooth = false; //是否平滑
    [SerializeField] private float smoothTime = 5f; //平滑速度
    [Header("角色移动")]
    [SerializeField] private float walkSpeed = 3; //走路速度
    [SerializeField] private float runSpeed = 5; //跑步速度
    [SerializeField] private float jumpSpeed = 3;//跳跃速度
    [SerializeField] private float stickToGroundForce = 10;
    [SerializeField] private float gravityMultiplier = 1;

    private CharacterController characterController; //角色控制器
    private Quaternion characterTargetRotation; //角色目标旋转
    private Quaternion cameraTargetRotation; //相机目标旋转

    private bool jump = false;
    private Vector2 input;
    private Vector3 moveDirection = Vector3.zero;
    private bool previouslyGrounded;
    private bool jumping = false;
    private bool isWalking = false;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); //获取角色控制器
        characterTargetRotation = transform.localRotation;
        cameraTargetRotation = headTransform.localRotation;
    }

    //Update函数，主要是获取输入数据
    private void Update()
    {
 

        //获取AD键或左右方向键输入
        float horizontal = Input.GetAxis("Horizontal");
        //获取WS键或上下方向键输入
        float vertical = Input.GetAxis("Vertical");
        //保存到字段
        input = new Vector2(horizontal, vertical);
        //获取是否为跑动
        isWalking = !Input.GetKey(KeyCode.LeftShift);
        //旋转视角
        RotateView(transform, headTransform);

        if (!jump) jump = Input.GetKeyDown(KeyCode.Space) && !Cursor.visible;
        if (!previouslyGrounded && characterController.isGrounded) //如果上一帧不在地面，这帧在地上，标记不在跳跃状态中
        {
            moveDirection.y = 0f; 
            jumping = false;
        }

        if (!characterController.isGrounded && !jumping && previouslyGrounded) //如果上一帧在地面，这一帧不在地面，且不在跳跃
        {
            moveDirection.y = 0f;
        }

        //保存该帧是否在地面的信息
        previouslyGrounded = characterController.isGrounded;
    }

    private void FixedUpdate()
    {
        float speed;

        if (Cursor.visible)
        {
            return;
        }
        //判断速度是跑还是走
        speed = isWalking ? walkSpeed : runSpeed;
        //将向量单位为1
        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }
        //计算目标移动向量
        Vector3 desiredMove = transform.forward * input.y + transform.right * input.x;

        RaycastHit hitInfo;
        //获取地面法线信息
        Physics.SphereCast(transform.position, characterController.radius, Vector3.down, out hitInfo,
                           characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        //计算移动方向
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
        //添加速度
        moveDirection.x = desiredMove.x * speed;
        moveDirection.z = desiredMove.z * speed;

        //如果在地面
        if (characterController.isGrounded)
        {
            moveDirection.y = -stickToGroundForce; //添加一个下降值

            if (jump) //如果点击跳跃了
            {
                moveDirection.y = jumpSpeed; //
                jump = false;
                jumping = true;
            }
        }
        else //如果在空中,添加重力
        {
            moveDirection += Physics.gravity * gravityMultiplier * Time.fixedDeltaTime;
        }

        //角色控制器移动
        characterController.Move(moveDirection * Time.fixedDeltaTime);

    }


    public void RotateView(Transform character, Transform camera)
    {

        if (Cursor.visible)
        {
            return;
        }
        float yRot = Input.GetAxis("Mouse X") * xSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * ySensitivity;

        characterTargetRotation *= Quaternion.Euler(0f, yRot, 0f);
        cameraTargetRotation *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation) //如果限制纵向旋转
            cameraTargetRotation = ClampRotationAroundXAxis(cameraTargetRotation);

        if (smooth) //如果需要平滑处理
        {
            //位置更新平滑
            character.localRotation = Quaternion.Slerp(character.localRotation, cameraTargetRotation,
                smoothTime * Time.deltaTime);
            //镜头更新平滑
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraTargetRotation,
                smoothTime * Time.deltaTime);
        }
        else //如果不需要平滑，直接赋值
        {
            character.localRotation = characterTargetRotation;
            camera.localRotation = cameraTargetRotation;
        }

    
    }

 

    //限制围绕x轴进行的旋转
    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minimumX, maximumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }


  
}
