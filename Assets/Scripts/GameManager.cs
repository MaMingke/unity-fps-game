using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalTime = 60; //游戏总时长
    public int score = 0; //当前分数
    public int time = 0; //当前剩余时间
    public Text scoreText; //显示分数的Text组件
    public Text timeText; //显示剩余时间的Text组件
    public GameObject startGameObj; //开始游戏界面
    public Button startButton; //开始游戏按钮
    public GameObject gameSummaryObj; //游戏结算UI游戏对象
    public Text summaryScoreText; //游戏结算界面里的分数
    public Button retryButton; //重新尝试按钮
    public Button quitButton; //退出按钮

    private float timer = 0; //计时器，保存每帧时间，累计到1秒重置
    private bool isPlaying = false; //是否正在游戏中
   

    public static GameManager Instance; //静态单例对象，用于全局调用GameManager实例

    private void Awake()
    {
        Instance = this; //最优先初始化实例
        Time.timeScale = 1; 
        startButton.onClick.AddListener(StartGame); //监听点击开始游戏按钮事件
        retryButton.onClick.AddListener(Retry); //监听点击重试按钮事件
        quitButton.onClick.AddListener(Quit); //监听点击退出按钮事件

        startGameObj.SetActive(true); //初始显示开始游戏界面
        gameSummaryObj.SetActive(false); //初始隐藏游戏结算UI
        time = totalTime; //初始设置游戏倒计时
    }

  

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime; //累计每帧时间
        }
        if (timer >= 1) //累计时间达到1秒
        {
            time --; //时间减少1
            timer = 0; //重置计时器
        }
        timeText.text = time.ToString(); //更新时间UI组件显示
        scoreText.text = score.ToString(); //更新分数UI组件显示

        if (time == 0) //如果时间为0
        {
            isPlaying = false;
            gameSummaryObj.SetActive(true); //显示游戏结算
            summaryScoreText.text = "Score: " + score.ToString(); //显示结算得分
            Cursor.visible = true; //显示鼠标
            Cursor.lockState = CursorLockMode.None; //解锁鼠标
            Time.timeScale = 0; //游戏时间暂停
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(0) && !startGameObj.activeSelf && !gameSummaryObj.activeSelf && !EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //点击开始游戏按钮
    private void StartGame()
    {
        startGameObj.SetActive(false); //隐藏开始游戏界面
        Cursor.visible = false; //隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked; //锁定鼠标
        isPlaying = true;
    }

    //当点击重试按钮，重新加载本场景，重新开始
    private void Retry()
    {
        Time.timeScale = 1; //恢复游戏时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //重新加载本场景
    }

    //当点击退出按钮，退出程序
    private void Quit()
    {
        Application.Quit();
    }

}
