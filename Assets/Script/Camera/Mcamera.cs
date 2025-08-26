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
    [Header("プレイヤー設定")]
    [SerializeField] private GameObject player; // プレイヤーオブジェクト
    [SerializeField] private float distance_base = 5.0f; // カメラとプレイヤーの基本的な距離
    [SerializeField] private float distance_Chargebase = 2.5f; // カメラとプレイヤーの基本的な距離

    [field: SerializeField] public float mouseSensitivityX { get; set; } = 100.0f; // マウスの横方向の感度
    [field: SerializeField] public float mouseSensitivityY { get; set; } = 100.0f; // マウスの縦方向の感度
    [SerializeField] private float minZoomDistance = 1.0f; // 最小ズーム距離
    [SerializeField] private float maxVerticalAngle = 90.0f; // カメラの縦方向の最大角度
    [SerializeField] private LayerMask obstacleLayers; // 障害物のレイヤーマスク
    [SerializeField] private float transparencyDistance = 2.0f; // プレイヤーが半透明になる距離
    [SerializeField] private float transparentAlpha = 0.5f; // 半透明時のアルファ値
    [SerializeField] private string targetTag = "TargetTag"; // カメラが向くターゲットのタグ
    [SerializeField] private float duration = 0.3f; // フォーカス期間
    [Header("カメラ追跡設定")]
    [SerializeField] private float trackingSpeed = 10.0f; // カメラ追跡速度

    [Header("カメラオフセット")]
    [SerializeField] private Vector3 cameraOffset = new Vector3(2.0f, 0.0f, 0.0f); // カメラの位置オフセット

    private float rotation_hor; // カメラの横方向の回転
    private float rotation_ver; // カメラの縦方向の回転
    private Vector3 playertrack; // プレイヤーを追跡する位置
    private Material playerMaterial; // プレイヤーのマテリアル
    private Color originalColor; // プレイヤーの元の色
    private bool isCursorLocked = true; // カーソルがロックされているかどうか
    private bool isFocusing = false; // カメラがターゲットにフォーカスしているかどうか
    private Quaternion targetRotation; // 目標の回転
    public Transform orientation;//方向
    public Transform playerObj;//プレイヤー
    [Header("プレイヤーの回転速度")]
    public float rotationSpeed;

    [Header("引っこ抜きモード")]
    [SerializeField] private Bullet mori; // プレイヤーオブジェクト

    private bool isLeftClicking = false; // 左クリックされているかどうかのフラグ

    private bool controller = false;

    private float lerptime = 0f;

    TutorialTextManager tutorial;//チュートリアル用

    private float rotation_hor_tutorial; // カメラの横方向の回転
    private float rotation_ver_tutorial; // カメラの縦方向の回転

    void Start()
    {


        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }

        //option用
        AudioSource source = GetComponent<AudioSource>();
        OptionManager manager = GameObject.Find("OptionManager").GetComponent<OptionManager>();
        source.volume = manager.BGMValue;


        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        rotation_hor = 0f; // 横方向の回転を初期化
        rotation_ver = 0f; // 縦方向の回転を初期化
        playertrack = Vector3.zero; // プレイヤー追跡位置を初期化


        rotation_hor = player.transform.rotation.y;

        // プレイヤーのマテリアルを取得
        Renderer playerRenderer = player.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;
            originalColor = playerMaterial.color;
        }

        // カーソルを非表示にしてロックする: Escキーで解除
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // カメラの回転
        // 右スティックの入力を取得
        rotation_hor += Input.GetAxis("CineHorizontalController") * mouseSensitivityX * Time.deltaTime;
        rotation_ver += Input.GetAxis("CineVerticalController") * mouseSensitivityY * Time.deltaTime;

        rotation_hor += Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        rotation_ver -= Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if(tutorial.GetNowCount() == 0 && tutorial.GetTextCount() == 1)
            {

                rotation_hor_tutorial = rotation_hor;
                rotation_ver_tutorial = rotation_ver;
            }

            if(rotation_hor_tutorial != rotation_hor || rotation_ver_tutorial != rotation_ver)
            {
                //現在のミッション確認
                if (tutorial.GetNowCount() == 0 && tutorial.GetTextCount() == 2)
                {
                    if(tutorial.GetTextCount() == 2 && tutorial.GetNowCount() == 0)
                        tutorial.Count(1);

                    tutorial.TextCount(1);
                }
            }


        }

        
            

        // 右クリックが押された場合

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
                // 目標の回転に向かって徐々に回転
                float rotationSpeed = 1f; // 回転速度
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                // 回転が目標の回転に十分近いかどうかを確認
                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    // ターゲットにフォーカス完了
                    isFocusing = false;
                }
            }
        }

        // Rキーが押された場合
        if (Input.GetKeyDown(KeyCode.R))
        {
            FocusOnClosestTarget();
        }

        //引っこ抜きモードなら
        if (mori.GetBulletState() == Bullet.State.PULL || mori.GetBulletState() == Bullet.State.HEALPULL)
        {
            //刺さっている銛の方向を向く
            Vector3 vec = mori.transform.position;
            vec.y = 0f;
            playerObj.forward = Vector3.Slerp(playerObj.forward, vec, Time.deltaTime * rotationSpeed);
        }

        // ズーム（スクロール） FixedUpdateで行わない
       // distance_base -= Input.mouseScrollDelta.y * 0.5f;
        if (distance_base < minZoomDistance) distance_base = minZoomDistance;

        // カメラとプレイヤーの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // プレイヤーの透明度を調整
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


            // 垂直角度を-90度から+90度の範囲に制限
            rotation_ver = Mathf.Clamp(rotation_ver, -maxVerticalAngle, maxVerticalAngle);

            // 基準ベクトルを回転
            var rotation = Vector3.Normalize(new Vector3(0, 0.2f, -5)); // 基準ベクトルを正規化
            rotation = Quaternion.Euler(rotation_ver, rotation_hor, 0) * rotation; // ベクトルを回転

            // 床や障害物に当たるかチェック
            RaycastHit hit;
            float distance = distance_base; // デフォルトのズーム距離をコピー

            if (Physics.SphereCast(playertrack + Vector3.up * 1.7f, 0.5f, rotation, out hit, distance, obstacleLayers))
            {
                distance = hit.distance - 0.5f; // 距離を上書きし、少し余裕を持たせる
            }

            // カメラの回転を適用
            transform.rotation = Quaternion.Euler(rotation_ver, rotation_hor, 0); // クォータニオンを適用

            // カメラを回転させ、ズームを適用
            transform.position = playertrack + rotation * distance;

            // 首の高さに調整
            var necklevel = Vector3.up * 1.7f;
            transform.position += necklevel;

            // 左クリック中はカメラオフセットを適用
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

            // プレイヤーの位置を追跡
            playertrack = Vector3.Lerp(playertrack, player.transform.position, Time.deltaTime * trackingSpeed);


        }
    }

    private void FocusOnClosestTarget()
    {
        // タグで指定されたすべてのターゲットを見つける
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        // ターゲットが存在しない場合は処理を終了
        if (targets.Length == 0) return;

        // メインカメラを取得
        Camera cam = Camera.main;

        // 最も近いターゲットとその距離を記録するための変数を初期化
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // 各ターゲットに対して処理を行う
        foreach (GameObject target in targets)
        {
            // プレイヤーからターゲットへの方向ベクトルを計算
            Vector3 targetDirection = target.transform.position - player.transform.position;
            // ターゲットまでの距離を計算
            float distanceToTarget = targetDirection.magnitude;

            // ターゲットのワールド座標をビューポート座標に変換
            Vector3 viewPos = cam.WorldToViewportPoint(target.transform.position);
            // ターゲットがカメラの視野内にあるかを判定
            bool isInView = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;

            // ターゲットが視野内にあり、かつ最も近い場合に更新
            if (isInView && distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }

        // 最も近いターゲットが存在する場合、ターゲットに向かって回転を開始
        if (closestTarget != null)
        {
            StartCoroutine(SmoothRotateToTarget(closestTarget.transform.position));
        }

    }

    private IEnumerator SmoothRotateToTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - player.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up); // Y軸を上向きに向ける
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

        // 新しい基準点を設定
        transform.rotation = Quaternion.Euler(rotation_ver, rotation_hor, 0);
    }

    private void RotatePlayerToCameraDirection()
    {
        // カメラの前方ベクトルを取得
        Vector3 cameraForward = Camera.main.transform.forward;

        // カメラの前方ベクトルのy成分を無視して、地面と平行な方向に限定する
        cameraForward.y = 0f;

        // ベクトルの大きさを1にする（正規化）
        cameraForward.Normalize();

        // カメラの方向を向くようにプレイヤーの方向を設定
        if (cameraForward != Vector3.zero)
        {
            if (mori.GetBulletState() != Bullet.State.PULL || mori.GetBulletState() != Bullet.State.HEALPULL)
            {
                // プレイヤーの前方ベクトルをカメラの方向に設定
                playerObj.forward = cameraForward;
            }
        }
    }

    public void CameraFunction()
    {
        // カメラのビュー方向を取得
        // 方向を回転
        Vector3 viewDir = playerObj.position - new Vector3(transform.position.x, playerObj.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // プレイヤーオブジェクトを回転
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