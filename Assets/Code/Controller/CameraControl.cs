using Code.Controller.CameraAction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Controller
{
    public class CameraControl : MonoBehaviour, ICameraAction
    {
        #region 环境

        [SerializeField] private new GameObject camera;
        [SerializeField] private GameObject player;
        [SerializeField] private float mouseSensitivity;
        private Controller controller;
        private float cameraH, cameraV;

        #endregion

        #region 初始化

        private void Awake()
        {
            controller = new Controller();
        }

        private void OnEnable()
        {
            controller.Enable();
        }

        private void OnDisable()
        {
            controller.Disable();
        }

        #endregion

        #region 处理

        private void LateUpdate()
        {
            ((ICameraAction)this).CameraMove();
        }

        private Vector3 GetMousePosition()
        {
            return new Vector3(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());
        }

        #endregion

        #region 动作

        void ICameraAction.CameraMove()
        {
            cameraH += GetMousePosition().x * Time.deltaTime * mouseSensitivity;
            cameraV -= GetMousePosition().y * Time.deltaTime * mouseSensitivity;
            cameraV = Mathf.Clamp(cameraV, -70, 40);
            var cameraR = new Vector3(cameraV, cameraH, 0);
            var playerR = new Vector3(0, cameraH, 0);
            transform.eulerAngles = Vector3.Lerp(camera.transform.eulerAngles, cameraR, 1f);
            player.transform.eulerAngles = Vector3.Lerp(player.transform.eulerAngles, playerR, 1f);
        }

        #endregion
    }
}