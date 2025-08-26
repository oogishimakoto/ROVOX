#undef UNICODE // Unicode�ł͂Ȃ��A�}���`�o�C�g�������g��

//----------------------------------------------------------------------------------
// ���C���C���N���[�h
#include "winmain.h"
#include <iostream>     // cout
#include <ctime>        // time
#include <cstdlib>      // srand,rand

//----------------------------------------------------------------------------------
// ���������[�N�𒲂ׂ�ׂɕK�v
#include "crtdbg.h"
#include <functional>
#define malloc(X) _malloc_dbg(X,_NORMAL_BLOCK,__FILE__,__LINE__) 
#define new ::new(_NORMAL_BLOCK, __FILE__, __LINE__)

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR IpCmdLine, int nCmdShow)
{
#ifdef _DEBUG
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
#endif // _DEBUG
	//����������
	srand((unsigned int)time(NULL));

	//�E�B���h�E�̃p�����[�^�����߂āA�o�^
	WNDCLASSEX wc;

	wc.cbSize = sizeof(WNDCLASSEX);
	wc.style = CS_CLASSDC;
	wc.lpfnWndProc = WndProc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = hInstance;
	wc.hIcon = NULL;
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wc.lpszMenuName = NULL;
	wc.lpszClassName = CLASS_NAME;
	wc.hIconSm = NULL;

	RegisterClassEx(&wc);

	//�E�B���h�E�����֐�
	HWND hWnd;
	hWnd = CreateWindowEx(0,// �g���E�B���h�E�X�^�C��
		CLASS_NAME,// �E�B���h�E�N���X�̖��O
		WINDOW_NAME,// �E�B���h�E�̖��O
		//WS_OVERLAPPEDWINDOW,// �E�B���h�E�X�^�C��
		WS_POPUP | WS_SYSMENU | WS_MINIMIZEBOX,// �E�B���h�E�X�^�C����ʃT�C�Y�Œ�		�~�{�^������	�ŏ�������
		CW_USEDEFAULT,// �E�B���h�E�̍���w���W
		CW_USEDEFAULT,// �E�B���h�E�̍���x���W 
		SCREEN_WIDTH,// �E�B���h�E�̕�
		SCREEN_HEIGHT,// �E�B���h�E�̍���
		NULL,// �e�E�B���h�E�̃n���h��
		NULL,// ���j���[�n���h���܂��͎q�E�B���h�EID
		hInstance,// �C���X�^���X�n���h��
		NULL);// �E�B���h�E�쐬�f�[�^

	// �w�肳�ꂽ�E�B���h�E�̕\����Ԃ�ݒ�(�E�B���h�E��\��)
	ShowWindow(hWnd, nCmdShow);
	// �E�B���h�E�̏�Ԃ𒼂��ɔ��f(�E�B���h�E�̃N���C�A���g�̈���X�V)
	UpdateWindow(hWnd);

	//Direct3D�̏������֐����Ăяo��
	Direct3D_Init(hWnd);

	//----------------------------------------------------------------------------------
	// �}�E�X�n
	// �}�E�X�J�[�\���̔�\��
	ShowCursor(FALSE);

	// �J�[�\���̈ʒu�擾
	GetCursorPos(&cursor);
	// �J�[�\���̈ʒu���Z�b�g����
	SetCursorPos(0.0f, 0.0f);

	//----------------------------------------------------------------------------------
	// FPS����ŕK�v�ȏ��擾
	//���ݎ������}�C�N���b�Ŏ擾
	std::function<long long(void)> currentTimeMicro = []()
	{
		std::chrono::system_clock::duration d = std::chrono::system_clock::now().time_since_epoch();
		return std::chrono::duration_cast<std::chrono::microseconds>(d).count();
	};

	//���ݎ������擾(1�b=1000000)
	long long end = currentTimeMicro();

	//���̍X�V���Ԃ��v�Z(1�b/�t���[�����[�g)
	long long next = end + (1000 * 1000 / fps);

	//----------------------------------------------------------------------------------
	// ������
	HDC hdc = GetDC(hWnd);

	// �T�E���h������
	XA_Initialize();

	std::srand(time(NULL));

	//----------------------------------------------------------------------------------
	//���b�Z�[�W���[�v
	MSG msg = {};
	// ���C�����[�v
	for (;;)
	{
		// �O��̃��[�v���烆�[�U�[���삪�����������ׂ�
		BOOL doesMessageExist = PeekMessage(&msg, NULL, 0, 0, PM_REMOVE);

		if (doesMessageExist)
		{ // ���[�U�[���삪�������ꍇ

			// �ԐړI�ɃE�C���h�E�v���V�[�W�����Ăяo��
			DispatchMessage(&msg);
			TranslateMessage(&msg);
			// �A�v���I�����߂�����
			if (msg.message == WM_QUIT)
			{
				// �I��
				break;
			}
		}
		else
		{
			//�Q�[���̏������L�q
			//DirectX�̕`�揈���Ȃǂ������ɋL�q����
			{
				timeBeginPeriod(1); // ���x���P�~���b�ɏグ��

				cnt++;				// fps�v�Z�p���Z�ϐ��ɉ��Z���Ă���

				//fps�`��
				std::string* fpsStr = fr.update();
				const char* fpsStrResult = fpsStr->c_str();
				TextOut(hdc, 10, 1000, fpsStrResult, fpsStr->size());

				//�d���������������ꍇ
				std::this_thread::sleep_for(std::chrono::milliseconds(5));	// �������~�߂�

				//�ł��邾��60fps�ɂȂ�悤�ɃX���b�h�ҋ@
				end = currentTimeMicro();
				if (end < next)
				{
					//�X�V���Ԃ܂őҋ@
					std::this_thread::sleep_for(std::chrono::microseconds(next - end));

					//���̍X�V���Ԃ��v�Z(1�b/�t���[�����[�g���Z)
					next += (1000 * 1000 / fps);
				}
				else
				{
					//�X�V���Ԃ��߂����ꍇ�͌��ݎ������玟�̍X�V���Ԃ��v�Z
					next = end + (1000 * 1000 / fps);
				}
				gDeltaTime = next;	// �f���^�^�C���ɌŒ�fps�̍����n��
				timeEndPeriod(1);	// ���x�����ɖ߂�
			}

			// �J�[�\���̈ʒu�𖈃t���[���擾����
			GetCursorPos(&cursor);

			Game.Initialize();	// �V�[���X�V
			Game.Update();		// �X�V����
			Game.Draw();		// �`�揈��	

			Input_Refresh(); // �L�[��Ԃ̍X�V

			if (endcheck == true)
				break;
		}
	}

	// �T�E���h���
	XA_Release();

	Game.Relesase();	// �������

	//Direct3D�̉���֐����Ăяo��
	Direct3D_Release();

	//���[�v�𔲂�����A�v���̏I������������
	UnregisterClass(CLASS_NAME, hInstance);

	return (int)msg.wParam;
}

// �E�C���h�E�v���V�[�W���֐������
// ���֐�������̂̓O���[�o���̈�(=�ǂ̊֐��̒��ł��Ȃ��ꏊ)�����I
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	// uMsg�i���̊֐��̑�2�����j���A���[�U�[�����ID���
	switch (uMsg)
	{
	case WM_DESTROY:// �E�B���h�E�j���̃��b�Z�[�W
		PostQuitMessage(0);// �gWM_QUIT�h���b�Z�[�W�𑗂�@���@�A�v���I��
		break;

	case WM_CLOSE:  // x�{�^���������ꂽ��
		DestroyWindow(hWnd);  // �gWM_DESTROY�h���b�Z�[�W�𑗂�
		break;

	case WM_LBUTTONDOWN: // ���N���b�N���ꂽ�Ƃ�
		Input_SetKeyDown(VK_LBUTTON);
		clickCheck = true;
		break;

	case WM_LBUTTONUP: // ���N���b�N���ꂽ�Ƃ�
		Input_SetKeyUp(VK_LBUTTON);
		clickCheck = false;
		break;

	case WM_RBUTTONDOWN: // �E�N���b�N���ꂽ�Ƃ�
		Input_SetKeyDown(VK_RBUTTON);
		break;

	case WM_RBUTTONUP: // �E�N���b�N���ꂽ�Ƃ�
		Input_SetKeyUp(VK_RBUTTON);
		break;

	case WM_MOUSEMOVE: // �}�E�X�J�[�\�����������Ƃ�

		// �J�[�\���̈ʒu�擾
		GetCursorPos(&cursor);

		break;

	case WM_KEYDOWN:
		// �L�[�������ꂽ���̃��A�N�V����������
		// ESC�������ꂽ�̂��ǂ����`�F�b�N
		if (LOWORD(wParam) == VK_ESCAPE)
		{
			// ���b�Z�[�W�{�b�N�X�ŏC���m�F
			int result;
			result = MessageBox(NULL, "�I�����Ă�낵���ł����H",
				"�I���m�F", MB_YESNO | MB_ICONQUESTION);
			if (result == IDYES) // �u�͂��v�{�^���������ꂽ��
			{
				// x�{�^���������ꂽ�̂Ɠ������ʂ𔭊�����
				PostMessage(hWnd, WM_CLOSE, wParam, lParam);
			}
		}
		Input_SetKeyDown(LOWORD(wParam));
		break;

	case WM_KEYUP: // �L�[�������ꂽ�C�x���g
		Input_SetKeyUp(LOWORD(wParam));
		break;

	default:
		// ���case�ȊO�̏ꍇ�̏��������s
		return DefWindowProc(hWnd, uMsg, wParam, lParam);
		break;
	}
	return 0;
}
