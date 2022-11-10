using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.Quaternion;
using static UnityEngine.Vector3;

public class GyroscopicRotation : MonoBehaviour
{
    //陀螺图片 
    public Transform gyro;
    
    //结算界面
    //public GameObject settlementCanvas;
    //旋转圈数
    //public int totationNumber;

    //转速
    public Text speedText;

    //调整旋转快慢(摩擦力)
    public float friction;

    //方向
    private float _direction;

    //初始向量
    private Vector3 _initialVector;

    //旋转速度,方向,接触屏幕时间
    private float _rotationSpeed, _rotationAngle, _contactTime;

    //是否继续旋转
    private bool _isRotating;

    void Update()
    {
        //点击屏幕
        if (Input.GetMouseButton(0))
        {
            _contactTime += Time.deltaTime;
            var gyroPosition = gyro.position;
            //判断方向
            _direction = Cross(_initialVector, Input.mousePosition - gyroPosition).z > 0 ? 1f : -1f;
            //旋转角度
            _rotationAngle = Angle(_initialVector, (Input.mousePosition - gyroPosition)) / friction;
            //旋转
            gyro.rotation *= AngleAxis(_direction * Angle(_initialVector, (Input.mousePosition - gyroPosition)), forward);
            //转速
            _rotationSpeed = _rotationAngle / _contactTime * 60 / friction;
            //显示
            speedText.text = _rotationAngle != 0f? ((int) (_rotationAngle / _contactTime * 60 / friction)).ToString() : "0";
            _initialVector = Input.mousePosition - gyroPosition;
        }

        //抬起，离开屏幕
        if (Input.GetMouseButtonUp(0))
        {
            _initialVector = zero;
            _isRotating = true;
        }

        //是否在继续旋转
        if (!_isRotating) return;
        if (_rotationSpeed > 0)
        {
            _rotationAngle -= 0.1f;
            gyro.rotation *= AngleAxis(_direction * _rotationAngle, forward);
            _rotationSpeed = _rotationAngle / _contactTime * 60 / friction;
            speedText.text = ((int) _rotationSpeed).ToString();
        }
        else
        {
            _isRotating = false;
            speedText.text = "0";
        }
    }
}