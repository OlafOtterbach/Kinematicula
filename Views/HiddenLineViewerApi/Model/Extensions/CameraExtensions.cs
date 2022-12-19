using Kinematicula.LogicViewing;
using Kinematicula.Mathematics.Extensions;

namespace HiddenLineViewerApi
{
    public static class CameraExtensions
    {
        public static CameraDto ToCameraDto(this CameraInfo cameraInfo)
        {
            var cameraDto = new CameraDto();

            cameraDto.Name = cameraInfo.Name;

            cameraDto.NearPlane = cameraInfo.NearPlane;

            cameraDto.Distance = cameraInfo.Distance;

            cameraDto.Target = cameraInfo.Target.ToPositionDto();

            cameraDto.Frame = cameraInfo.Frame.ToCardanFrame().ToCardanFrameDto();

            return cameraDto;
        }

        public static CameraInfo ToCameraInfo(this CameraDto cameraDto)
        {
            var camera = new CameraInfo();
            camera.Name = cameraDto.Name;
            camera.NearPlane = cameraDto.NearPlane;
            camera.Distance = cameraDto.Distance;
            camera.Target = cameraDto.Target.ToPosition3D();
            camera.Frame = cameraDto.Frame.ToCardanFrame().ToMatrix44D();

            return camera;
        }
    }
}