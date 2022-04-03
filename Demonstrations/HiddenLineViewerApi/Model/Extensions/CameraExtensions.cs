using Kinematicula.LogicViewing;
using Kinematicula.Mathematics.Extensions;

namespace HiddenLineViewerApi
{
    public static class CameraExtensions
    {
        public static CameraDto ToCameraDto(this CameraInfo camera)
        {
            var cameraDto = new CameraDto();

            cameraDto.Name = camera.Name;

            cameraDto.NearPlane = camera.NearPlane;

            cameraDto.Distance = camera.Distance;

            cameraDto.TargetDistance = (camera.Target - camera.Frame.Offset).Length;

            cameraDto.Frame = camera.Frame.ToCardanFrame().ToCardanFrameDto();

            return cameraDto;
        }

        public static CameraInfo ToCamera(this CameraDto cameraDto)
        {
            var frame = cameraDto.Frame.ToCardanFrame().ToMatrix44D();

            var position = frame.Offset;
            var direction = frame.Ey;
            var target = position + direction * cameraDto.TargetDistance;

            var camera = new CameraInfo();
            camera.Name = cameraDto.Name;
            camera.NearPlane = cameraDto.NearPlane;
            //camera.Distance = cameraDto.Distance;
            camera.Frame = frame;
            camera.Target = target;
            camera.NearPlane = 1.0;

            return camera;
        }
    }
}