// using UnityEngine;

// public class WalkingAroundTest : MonoBehaviour
// {
//     [SerializeField] private Enemy _enemy;
//     [SerializeField] private Transform _playerTrm;
//     [SerializeField] private float _walkingDir;

//     [Header("Around Setting")]
//     [SerializeField] private float _range;
//     [SerializeField] private float _walkingLimits;
//     [SerializeField] private float _walkSpeed;
//     [SerializeField] private bool isRun;

//     private WalkingDir _currentDir;
//     private float _originAngle = 0f;
//     private float _currentValue;
//     private Vector3 _playerPos = Vector3.zero;
//     private Vector3 _playerDir = Vector3.zero;
//     private void SettingValues()
//     {
//         _currentDir = (WalkingDir)Random.Range(0, 2);
//         _walkingDir = (float)_currentDir;
//         _playerPos = _playerTrm.position;
//         _playerDir = _playerTrm.right;

//         Vector3 dirToOwner = transform.position - _playerPos;
//         dirToOwner.y = 0;
//         _playerDir.y = 0;
//         _originAngle = Mathf.Acos(Vector3.Dot(dirToOwner.normalized, _playerDir.normalized)) * Mathf.Rad2Deg;

//         if(Vector3.Cross(dirToOwner, _playerDir.normalized).y < 0)
//             _originAngle = 360 - _originAngle;

//         isRun = true;
//         _enemy.AnimatorCompo.SetFloat("WalkingDir", (float)_currentDir);
//     }
//     private void Update()
//     {
//         if(!isRun){
//             SettingValues(); // 값 초기화
//             isRun = true;
//         }
//         _enemy.MovementCompo.LookToTarget(_playerTrm.position); // 대상 바라보기
//         float x = 0f, z = 0f;
//         Vector3 point;

//         _currentValue += Time.deltaTime * _walkSpeed;
//         if(_currentValue >= _walkingLimits){
//             _currentValue = 0;
//             isRun = false;
//             return;
//         }

//         switch(_currentDir){
//             case WalkingDir.Left: // + 
//             x = Mathf.Cos((_originAngle - _currentValue) * Mathf.Deg2Rad) * _range;
//             z = Mathf.Sin((_originAngle - _currentValue) * Mathf.Deg2Rad) * _range;
//             break;
//             case WalkingDir.Right: // -
//             x = Mathf.Cos((_originAngle + _currentValue) * Mathf.Deg2Rad) * _range;
//             z = Mathf.Sin((_originAngle + _currentValue) * Mathf.Deg2Rad) * _range;
//             break;
//         }
//         point = new Vector3(x + _playerPos.x, 0, z + _playerPos.z);
//         _enemy.MovementCompo.SetDirectMovement(point, false); // 넣어준 Vector3 값으로 이동해주는 함수
//         return;
//     }
// }
