using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Vector2 targetPosition; // 目标位置
    public float initialSpeed;     // 初始速度

    private Rigidbody2D rb;        // Rigidbody2D组件
    private float distanceToTarget; // 到目标的距离
    private float timeToTravel;    // 预计旅行时间
    private float deceleration;    // 减速度

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D组件未附加到物体上。");
            return;
        }

        // 计算到目标的距离
        distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        // 计算需要的减速度，使得物体在到达目标时速度为0
        // 使用公式 v^2 = u^2 + 2as，其中v是最终速度(0)，u是初始速度，a是加速度(减速度的相反数)，s是距离
        // 0 = initialSpeed^2 + 2 * deceleration * distanceToTarget
        deceleration = -(initialSpeed * initialSpeed) / (2 * distanceToTarget);

        // 计算旅行时间，使用公式 s = ut + 0.5at
        // 这里我们解出时间 t = (-u + sqrt(u^2 + 2as)) / a
        timeToTravel = (-initialSpeed + Mathf.Sqrt(initialSpeed * initialSpeed + 2 * deceleration * distanceToTarget)) / deceleration;

        // 设置初始速度
        rb.velocity = new Vector2(initialSpeed, rb.velocity.y);
    }

    void Update()
    {
        // 应用减速度
        rb.velocity += new Vector2(deceleration * Time.deltaTime, 0) * Time.deltaTime;

        // 移动物体向目标位置靠近
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, initialSpeed * Time.deltaTime));

        // 检查是否到达目标位置
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f) // 0.1f作为容错范围
        {
            rb.velocity = Vector2.zero; // 确保速度为0
            Debug.Log("物体已到达目标位置并停止。");
        }
    }
}