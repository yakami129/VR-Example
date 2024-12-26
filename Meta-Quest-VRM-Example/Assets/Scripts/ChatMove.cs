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
        // 初始化随机方向
        ChangeDirection();
        Debug.Log("ChatMove已启动");
    }

    void Update()
    {
        // 计时器更新
        timer += Time.deltaTime;
        
        // 每隔一段时间改变方向
        if (timer >= changeDirectionInterval)
        {
            ChangeDirection();
            timer = 0f;
        }

        // 移动角色，朝向当前方向
        transform.LookAt(transform.position + currentDirection);
        transform.position += currentDirection * speed * Time.deltaTime;
        
        ChangeAnimation();
    }

    private void ChangeDirection()
    {
        // 生成随机方向（x和z平面上）
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        currentVelocity = new Vector3(randomX, 0, randomZ);
        currentDirection = new Vector3(randomX, 0, randomZ).normalized;
        
        Debug.Log($"方向已改变 - 新方向: X={randomX:F2}, Z={randomZ:F2}");
        Debug.Log($"当前速度大小: {currentVelocity.magnitude:F2}");
    }

    private void ChangeAnimation()
    {
        animator.SetFloat("speed", currentVelocity.magnitude);
        Debug.Log($"动画速度已更新: {currentVelocity.magnitude:F2}");
    }
}
