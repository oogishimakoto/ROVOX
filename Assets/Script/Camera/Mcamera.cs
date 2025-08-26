using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAction;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Mcamera : MonoBehaviour
{
    [Header("�v���C���[�ݒ�")]
    [SerializeField] private GameObject player; // �v���C���[�I�u�W�F�N�g
    [SerializeField] private float distance_base = 5.0f; // �J�����ƃv���C���[�̊�{�I�ȋ���
    [SerializeField] private float distance_Chargebase = 2.5f; // �J�����ƃv���C���[�̊�{�I�ȋ���

    [field: SerializeField] public float mouseSensitivityX { get; set; } = 100.0f; // �}�E�X�̉������̊��x
    [field: SerializeField] public float mouseSensitivityY { get; set; } = 100.0f; // �}�E�X�̏c�����̊��x
    [SerializeField] private float minZoomDistance = 1.0f; // �ŏ��Y�[������
    [SerializeField] private float maxVerticalAngle = 90.0f; // �J�����̏c�����̍ő�p�x
    [SerializeField] private LayerMask obstacleLayers; // ��Q���̃��C���[�}�X�N
    [SerializeField] private float transparencyDistance = 2.0f; // �v���C���[���������ɂȂ鋗��
    [SerializeField] private float transparentAlpha = 0.5f; // ���������̃A���t�@�l
    [SerializeField] private string targetTag = "TargetTag"; // �J�����������^�[�Q�b�g�̃^�O
    [SerializeField] private float duration = 0.3f; // �t�H�[�J�X����
    [Header("�J�����ǐՐݒ�")]
    [SerializeField] private float trackingSpeed = 10.0f; // �J�����ǐՑ��x

    [Header("�J�����I�t�Z�b�g")]
    [SerializeField] private Vector3 cameraOffset = new Vector3(2.0f, 0.0f, 0.0f); // �J�����̈ʒu�I�t�Z�b�g

    private float rotation_hor; // �J�����̉������̉�]
    private float rotation_ver; // �J�����̏c�����̉�]
    private Vector3 playertrack; // �v���C���[��ǐՂ���ʒu
    private Material playerMaterial; // �v���C���[�̃}�e���A��
    private Color originalColor; // �v���C���[�̌��̐F
    private bool isCursorLocked = true; // �J�[�\�������b�N����Ă��邩�ǂ���
    private bool isFocusing = false; // �J�������^�[�Q�b�g�Ƀt�H�[�J�X���Ă��邩�ǂ���
    private Quaternion targetRotation; // �ڕW�̉�]
    public Transform orientation;//����
    public Transform playerObj;//�v���C���[
    [Header("�v���C���[�̉�]���x")]
    public float rotationSpeed;

    [Header("�������������[�h")]
    [SerializeField] private Bullet mori; // �v���C���[�I�u�W�F�N�g

    private bool isLeftClicking = false; // ���N���b�N����Ă��邩�ǂ����̃t���O

    private bool controller = false;

    private float lerptime = 0f;

    TutorialTextManager tutorial;//�`���[�g���A���p

    private float rotation_hor_tutorial; // �J�����̉������̉�]
    private float rotation_ver_tutorial; // �J�����̏c�����̉�]

    void Start()
    {


        //�`���[�g���A���p
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }

        //option�p
        AudioSource source = GetComponent<AudioSource>();
        OptionManager manager = GameObject.Find("OptionManager").GetComponent<OptionManager>();
        source.volume = manager.BGMValue;


        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        rotation_hor = 0f; // �������̉�]��������
        rotation_ver = 0f; // �c�����̉�]��������
        playertrack = Vector3.zero; // �v���C���[�ǐՈʒu��������


        rotation_hor = player.transform.rotation.y;

        // �v���C���[�̃}�e���A�����擾
        Renderer playerRenderer = player.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;
            originalColor = playerMaterial.color;
        }

        // �J�[�\�����\���ɂ��ă��b�N����: Esc�L�[�ŉ���
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // �J�����̉�]
        // �E�X�e�B�b�N�̓��͂��擾
        rotation_hor += Input.GetAxis("CineHorizontalController") * mouseSensitivityX * Time.deltaTime;
        rotation_ver += Input.GetAxis("CineVerticalController") * mouseSensitivityY * Time.deltaTime;

        rotation_hor += Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        rotation_ver -= Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        //�`���[�g���A���p
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if(tutorial.GetNowCount() == 0 && tutorial.GetTextCount() == 1)
            {

                rotation_hor_tutorial = rotation_hor;
                rotation_ver_tutorial = rotation_ver;
            }

            if(rotation_hor_tutorial != rotation_hor || rotation_ver_tutorial != rotation_ver)
            {
                //���݂̃~�b�V�����m�F
                if (tutorial.GetNowCount() == 0 && tutorial.GetTextCount() == 2)
                {
                    if(tutorial.GetTextCount() == 2 && tutorial.GetNowCount() == 0)
                        tutorial.Count(1);

                    tutorial.TextCount(1);
                }
            }


        }

        
            

        // �E�N���b�N�������ꂽ�ꍇ

        if (!controller)
        {
            isLeftClicking = Input.GetMouseButton(0);
        }

           

        if (isLeftClicking)
        {
            
            RotatePlayerToCameraDirection();
        }
        else
        {
            CameraFunction();
            if (isFocusing)
            {
                // �ڕW�̉�]�Ɍ������ď��X�ɉ�]
                float rotationSpeed = 1f; // ��]���x
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                // ��]���ڕW�̉�]�ɏ\���߂����ǂ������m�F
                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    // �^�[�Q�b�g�Ƀt�H�[�J�X����
                    isFocusing = false;
                }
            }
        }

        // R�L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.R))
        {
            FocusOnClosestTarget();
        }

        //�������������[�h�Ȃ�
        if (mori.GetBulletState() == Bullet.State.PULL || mori.GetBulletState() == Bullet.State.HEALPULL)
        {
            //�h�����Ă�����̕���������
            Vector3 vec = mori.transform.position;
            vec.y = 0f;
            playerObj.forward = Vector3.Slerp(playerObj.forward, vec, Time.deltaTime * rotationSpeed);
        }

        // �Y�[���i�X�N���[���j FixedUpdate�ōs��Ȃ�
       // distance_base -= Input.mouseScrollDelta.y * 0.5f;
        if (distance_base < minZoomDistance) distance_base = minZoomDistance;

        // �J�����ƃv���C���[�̋������v�Z
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // �v���C���[�̓����x�𒲐�
        if (distanceToPlayer < transparencyDistance && playerMaterial != null)
        {
            Color color = playerMaterial.color;
            color.a = transparentAlpha;
            playerMaterial.color = color;
        }
        else if (playerMaterial != null)
        {
            playerMaterial.color = originalColor;
        }
    }

    void FixedUpdate()
    {
        if (isCursorLocked)
        {


            // �����p�x��-90�x����+90�x�͈̔͂ɐ���
            rotation_ver = Mathf.Clamp(rotation_ver, -maxVerticalAngle, maxVerticalAngle);

            // ��x�N�g������]
            var rotation = Vector3.Normalize(new Vector3(0, 0.2f, -5)); // ��x�N�g���𐳋K��
            rotation = Quaternion.Euler(rotation_ver, rotation_hor, 0) * rotation; // �x�N�g������]

            // �����Q���ɓ����邩�`�F�b�N
            RaycastHit hit;
            float distance = distance_base; // �f�t�H���g�̃Y�[���������R�s�[

            if (Physics.SphereCast(playertrack + Vector3.up * 1.7f, 0.5f, rotation, out hit, distance, obstacleLayers))
            {
                distance = hit.distance - 0.5f; // �������㏑�����A�����]�T����������
            }

            // �J�����̉�]��K�p
            transform.rotation = Quaternion.Euler(rotation_ver, rotation_hor, 0); // �N�H�[�^�j�I����K�p

            // �J��������]�����A�Y�[����K�p
            transform.position = playertrack + rotation * distance;

            // ��̍����ɒ���
            var necklevel = Vector3.up * 1.7f;
            transform.position += necklevel;

            // ���N���b�N���̓J�����I�t�Z�b�g��K�p
            if (mori.GetBulletState() == Bullet.State.NORMAL)
            {
                if (isLeftClicking)
                {
                    
                    transform.position = Vector3.Lerp(transform.position,transform.position + (transform.right * cameraOffset.x + transform.up * cameraOffset.y + transform.forward * cameraOffset.z), lerptime);
                    distance_base = Mathf.Lerp(distance_base, distance_Chargebase, 0.005f);
                    if(lerptime == 0)
                    {
                        lerptime += 0.001f;
                    }
                    else
                    {
                        lerptime += lerptime;
                    }
                    
                }
                else
                {
                    lerptime = 0;
                    distance_base =  Mathf.Lerp(distance_base, 4.5f, 0.1f);
                }
            }

            // �v���C���[�̈ʒu��ǐ�
            playertrack = Vector3.Lerp(playertrack, player.transform.position, Time.deltaTime * trackingSpeed);


        }
    }

    private void FocusOnClosestTarget()
    {
        // �^�O�Ŏw�肳�ꂽ���ׂẴ^�[�Q�b�g��������
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        // �^�[�Q�b�g�����݂��Ȃ��ꍇ�͏������I��
        if (targets.Length == 0) return;

        // ���C���J�������擾
        Camera cam = Camera.main;

        // �ł��߂��^�[�Q�b�g�Ƃ��̋������L�^���邽�߂̕ϐ���������
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // �e�^�[�Q�b�g�ɑ΂��ď������s��
        foreach (GameObject target in targets)
        {
            // �v���C���[����^�[�Q�b�g�ւ̕����x�N�g�����v�Z
            Vector3 targetDirection = target.transform.position - player.transform.position;
            // �^�[�Q�b�g�܂ł̋������v�Z
            float distanceToTarget = targetDirection.magnitude;

            // �^�[�Q�b�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ�
            Vector3 viewPos = cam.WorldToViewportPoint(target.transform.position);
            // �^�[�Q�b�g���J�����̎�����ɂ��邩�𔻒�
            bool isInView = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;

            // �^�[�Q�b�g��������ɂ���A���ł��߂��ꍇ�ɍX�V
            if (isInView && distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }

        // �ł��߂��^�[�Q�b�g�����݂���ꍇ�A�^�[�Q�b�g�Ɍ������ĉ�]���J�n
        if (closestTarget != null)
        {
            StartCoroutine(SmoothRotateToTarget(closestTarget.transform.position));
        }

    }

    private IEnumerator SmoothRotateToTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - player.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up); // Y����������Ɍ�����
        float startRotationHor = rotation_hor;
        float endRotationHor = targetRotation.eulerAngles.y;
        float startRotationVer = rotation_ver;
        float endRotationVer = targetRotation.eulerAngles.x;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rotation_hor = Mathf.LerpAngle(startRotationHor, endRotationHor, elapsedTime / duration);
            rotation_ver = Mathf.LerpAngle(startRotationVer, endRotationVer, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �V������_��ݒ�
        transform.rotation = Quaternion.Euler(rotation_ver, rotation_hor, 0);
    }

    private void RotatePlayerToCameraDirection()
    {
        // �J�����̑O���x�N�g�����擾
        Vector3 cameraForward = Camera.main.transform.forward;

        // �J�����̑O���x�N�g����y�����𖳎����āA�n�ʂƕ��s�ȕ����Ɍ��肷��
        cameraForward.y = 0f;

        // �x�N�g���̑傫����1�ɂ���i���K���j
        cameraForward.Normalize();

        // �J�����̕����������悤�Ƀv���C���[�̕�����ݒ�
        if (cameraForward != Vector3.zero)
        {
            if (mori.GetBulletState() != Bullet.State.PULL || mori.GetBulletState() != Bullet.State.HEALPULL)
            {
                // �v���C���[�̑O���x�N�g�����J�����̕����ɐݒ�
                playerObj.forward = cameraForward;
            }
        }
    }

    public void CameraFunction()
    {
        // �J�����̃r���[�������擾
        // ��������]
        Vector3 viewDir = playerObj.position - new Vector3(transform.position.x, playerObj.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // �v���C���[�I�u�W�F�N�g����]
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            if (mori.GetBulletState() != Bullet.State.PULL || mori.GetBulletState() != Bullet.State.HEALPULL)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                FocusOnClosestTarget();
                break;
        }
    }

    public void ShotForcus(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isLeftClicking = true;
                controller = true;
                break;
        }
    }

    public void ShotForcusEnd(InputAction.CallbackContext context)
    {
        isLeftClicking = false;
        controller = false;
    }
}