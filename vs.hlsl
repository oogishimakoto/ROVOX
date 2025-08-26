
// ���_�V�F�[�_�[�̃��C���֐�
//�����F�e���_���ꗥ�A�ړ��A��]�A�g��k������
// �߂�l�F���̃V�F�[�_�[�̏����̌��ʂ��A���_�̃f�[�^�Ƃ��Ė߂�
// ���� inputPos�FVRAM�ɓ]�����ꂽ���_�f�[�^�i�̂����P�j���n�����

//return�p�̍\����
struct VS_OUTPUT
{
	float4 pos : SV_Position;
	float2 uv : TEXCOORD;
	float4 col : COLOR;
};

VS_OUTPUT vs_main(float4 inputPos : POSITION,float2 uv : TEX,float4 col : COL)
{
	VS_OUTPUT output;

	output.pos = inputPos;  //return�p�̕ϐ��Ɉڂ��ւ���
	output.uv = uv;
	output.col = col;

	//�g��k���@��ʑS�̂ɍs���̂ŁA�J�����̃Y�[���@�\�̂悤�ɂȂ�
	/*output.pos.x *= 1.0f;
	output.pos.y *= 1.0f;*/

	//��ʂ������Ȃ̂ŕ\���̔䗦�𒲐�����
	output.pos.x *= 480.0f / 640.0f;
	output.pos.y *= 640.0f / 480.0f;
	//output.pos.x *= 1080.0f / 1920.0f;
	//output.pos.x *= 1.0f / 1.0f;

    return output;
}