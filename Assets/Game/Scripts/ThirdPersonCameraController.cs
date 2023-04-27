using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform _player; // объект игрока
    [SerializeField] private float _distance = 5.0f; // расстояние от камеры до игрока
    [SerializeField] private float _height = 2.0f; // высота камеры над игроком
    [SerializeField] private float _sensitivity = 3.0f; // чувствительность мыши
    [SerializeField] private float _limitUp = 80.0f; // ограничение угла поворота камеры по вертикали
    [SerializeField] private float _limitDown = -20.0f; // ограничение угла поворота камеры по вертикали
    
    private float rotationX = 0.0f; // текущий угол поворота камеры по горизонтали
    private float rotationY = 0.0f; // текущий угол поворота камеры по вертикали
    
    void Update()
    {
        // получаем значения движения мыши по осям X и Y
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
    
        // вычисляем новые углы поворота камеры
        rotationX += mouseX * _sensitivity;
        rotationY -= mouseY * _sensitivity;
        rotationY = Mathf.Clamp(rotationY, _limitDown, _limitUp);
    
        // поворачиваем камеру в соответствии с новыми углами
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
    
        // устанавливаем позицию камеры
        transform.position = _player.position - transform.rotation * Vector3.forward * _distance + Vector3.up * _height;
    }
}
