// �s�N�Z���V�F�[�_�[�̃��C���֐��@���@�|���S����`�悷��̂ɕK�v�ȃs�N�Z���������Ăяo�����
// �e�s�N�Z���̐F���w�肷��̂����
// �߂�l�F�@���̃s�N�Z�������F�ɂ��������ARGBA�Ŏw�肷��
// ����inputPos�F�@���_�̍��W�B�s�N�Z���V�F�[�_�[�ł͂��܂�Ӗ��������Ȃ��B

//�O���[�o���ϐ��@�V�F�[�_�[�̃O���[�o���ϐ��͓ǂݎ���p
//�e�N�X�`��
Texture2D gTexture : register(t0);

//�T���v���[
SamplerState gSampler : register(s0);

float4 ps_main( float4 inputPos : SV_POSITION,float2 uv : TEXCOORD ,float4 col : COLOR) : SV_Target
{
	//float color = inputPos.x / 640.0f;

	//float4 pixelColor = float4(color, color, color, 1.0f); // RGBA  0.0f-1.0f

	//if (inputPos.x < 320.0f)
	//{
	//	pixelColor = float4(1.0f, 0.0f, 1.0f, 1.0f); // RGBA  0.0f-1.0f
	//}
	//else
	//{
	//	pixelColor = float4(1.0f, 1.0f, 0.0f, 1.0f); // RGBA  0.0f-1.0f
	//}

	float4 pixelColor = gTexture.Sample(gSampler,uv);

	// ���_�̐F��������
	pixelColor *= col;

	return pixelColor;
}
