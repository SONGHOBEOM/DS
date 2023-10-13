using System.Collections;
using UnityEngine;
namespace PlayerCORE
{
    public class CameraMoveController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        public Camera mainCamera { get { return _mainCamera; } }
        [SerializeField, Range(1, 100)] private float sensitivity;
        [SerializeField, Range(1, 100)] private float smoothness;
        [SerializeField] private float clampAngle;
        [SerializeField, Range(1, 100)] private float minDistance;
        [SerializeField, Range(1, 100)] private float maxDistance;

        public Transform targetObject;

        private float rotX;
        private float rotY;

        private Quaternion initCamRotation;
        private bool onInit = false;

        private Vector3 dirNormalized;
        private Vector3 finalDir;
        private float finalDistance;
        private float followSpeed = 2000;

        private void Start()
        {
            rotX = transform.localRotation.eulerAngles.x;
            rotY = transform.localRotation.eulerAngles.y;

            dirNormalized = mainCamera.transform.localPosition.normalized;
            finalDistance = mainCamera.transform.localPosition.magnitude;
        }

        public void SetInitCameraRotation(Quaternion rotation)
        {
            onInit = true;
            initCamRotation = rotation;
        }
        private void SetCameraRotation()
        {
            onInit = false;
            transform.rotation = initCamRotation;
        }


        public void UpdateRotate()
        {
            StartCoroutine(RotateCamera());
            StartCoroutine(DistanceToTarget());
        }
        private IEnumerator RotateCamera()
        {
            while (true)
            {
                if (PlayerStateHelper.isAttacking == false && UIManager.Instance.onDragToRotate)
                {
                    float inputRotX = -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
                    float inputRotY = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

                    rotX += inputRotX; // 입력 방향을 target이 바라보도록 값을 음수로 받음.
                    rotY += inputRotY;

                    rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
                    Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
                    transform.rotation = rot;

                    yield return null;
                }
                else if (onInit)
                {
                    SetCameraRotation();
                    yield return null;
                }
                else
                    yield return null;
            }
        }

        private IEnumerator DistanceToTarget()
        {
            while (true)
            {
                if (targetObject == null) yield return null;

                transform.position = Vector3.MoveTowards(transform.position, targetObject.position, followSpeed * Time.deltaTime);
                finalDir = transform.TransformPoint(dirNormalized * maxDistance);

                RaycastHit hit;

                if (Physics.Linecast(transform.position, finalDir, out hit))
                {
                    finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
                }
                else
                    finalDistance = maxDistance;

                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

                yield return null;
            }
        }

    }
}
