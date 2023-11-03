using UnityEngine;
using Cinemachine;

[SaveDuringPlay] [AddComponentMenu("")]
public class SetCinemachineCameraPosition : CinemachineExtension
{
    public float xPos = 0;
 
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var position = state.RawPosition;
            position.x = xPos;
            state.RawPosition = position;
        }
    }
}