using UnityEngine;


public class ChatMove : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirectionInterval = 2f;
    private float timer;
    private Vector3 currentDirection;
    private Vector3 currentVelocity;

    public Animator animator;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
        Debug.Log("ChatMove已启动");
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");

        currentVelocity = new Vector3(x, 0, z);
        currentDirection = new Vector3(x, 0, z).normalized;
        
        Debug.Log($"方向已改变 - 新方向: X={x:F2}, Z={z:F2}");
        Debug.Log($"当前速度大小: {currentVelocity.magnitude:F2}");

        // 移动角色，朝向当前方向
        transform.LookAt(transform.position + currentDirection);
        transform.position += currentDirection * speed * Time.deltaTime;
        
        ChangeAnimation();
    }


    private void ChangeAnimation()
    {
        animator.SetFloat("speed", currentVelocity.magnitude);
        Debug.Log($"动画速度已更新: {currentVelocity.magnitude:F2}");
    }
}
