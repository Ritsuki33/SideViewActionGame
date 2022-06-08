using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion2 : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField,Tooltip("�p���x")] private float angleSpeed;
    [SerializeField, Tooltip("���a")] private float radius;

    [SerializeField, Tooltip("�ǐ����x")] private float followRatio;

    Velocity velocity;

    Rigidbody2D rb;
    private float currentRadius;
    [SerializeField]public float Angle { get; private set; }
    private float CurrentRadius
    {
        get
        {
            return currentRadius;
        }
        set
        {
            currentRadius = value;
           
        }
    }
    private void Start()
    {
        rb =GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target) return;

        CurrentRadius = Mathf.Lerp((this.transform.position - target.transform.position).magnitude, radius, followRatio);

        Angle += Time.deltaTime * angleSpeed;
        Angle %= 360;
        rb.MovePosition(
            target.transform.position 
            + new Vector3(CurrentRadius * Mathf.Cos(Angle), CurrentRadius * Mathf.Sin(Angle) )
        );

    }

    float FollowToValue(float currentValue,float targetValue,float followRatio)
    {
        float res = Mathf.Lerp(currentValue, targetValue, followRatio);
        return res;
    }
}
