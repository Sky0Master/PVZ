using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Vector2 targetPosition; // Ŀ��λ��
    public float initialSpeed;     // ��ʼ�ٶ�

    private Rigidbody2D rb;        // Rigidbody2D���
    private float distanceToTarget; // ��Ŀ��ľ���
    private float timeToTravel;    // Ԥ������ʱ��
    private float deceleration;    // ���ٶ�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D���δ���ӵ������ϡ�");
            return;
        }

        // ���㵽Ŀ��ľ���
        distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        // ������Ҫ�ļ��ٶȣ�ʹ�������ڵ���Ŀ��ʱ�ٶ�Ϊ0
        // ʹ�ù�ʽ v^2 = u^2 + 2as������v�������ٶ�(0)��u�ǳ�ʼ�ٶȣ�a�Ǽ��ٶ�(���ٶȵ��෴��)��s�Ǿ���
        // 0 = initialSpeed^2 + 2 * deceleration * distanceToTarget
        deceleration = -(initialSpeed * initialSpeed) / (2 * distanceToTarget);

        // ��������ʱ�䣬ʹ�ù�ʽ s = ut + 0.5at
        // �������ǽ��ʱ�� t = (-u + sqrt(u^2 + 2as)) / a
        timeToTravel = (-initialSpeed + Mathf.Sqrt(initialSpeed * initialSpeed + 2 * deceleration * distanceToTarget)) / deceleration;

        // ���ó�ʼ�ٶ�
        rb.velocity = new Vector2(initialSpeed, rb.velocity.y);
    }

    void Update()
    {
        // Ӧ�ü��ٶ�
        rb.velocity += new Vector2(deceleration * Time.deltaTime, 0) * Time.deltaTime;

        // �ƶ�������Ŀ��λ�ÿ���
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, initialSpeed * Time.deltaTime));

        // ����Ƿ񵽴�Ŀ��λ��
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f) // 0.1f��Ϊ�ݴ�Χ
        {
            rb.velocity = Vector2.zero; // ȷ���ٶ�Ϊ0
            Debug.Log("�����ѵ���Ŀ��λ�ò�ֹͣ��");
        }
    }
}