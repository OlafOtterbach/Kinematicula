﻿using Kinematicula.LogicViewing;
using Kinematicula.Mathematics.Extensions;

namespace HiddenLineViewerApi
{
    public static class CameraExtensions
    {
        public static CameraDto ToCameraDto(this CameraInfo camera)
        {
            var cameraDto = new CameraDto();

            cameraDto.TargetDistance = (camera.Target - camera.Frame.Offset).Length;

            cameraDto.Frame = camera.Frame.ToCardanFrame().ToCardanFrameDto();

            return cameraDto;
        }

        public static CameraInfo ToCamera(this CameraDto cameraDto)
        {
            //var frame = new Matrix44D(
            //    cameraDto.A11, cameraDto.A12, cameraDto.A13, cameraDto.A14,
            //    cameraDto.A21, cameraDto.A22, cameraDto.A23, cameraDto.A24,
            //    cameraDto.A31, cameraDto.A32, cameraDto.A33, cameraDto.A34,
            //    cameraDto.A41, cameraDto.A42, cameraDto.A43, cameraDto.A44);
            var frame = cameraDto.Frame.ToCardanFrame().ToMatrix44D();

            var position = frame.Offset;
            var direction = frame.Ey;
            var target = position + direction * cameraDto.TargetDistance;

            var camera = new CameraInfo();
            camera.Frame = frame;
            camera.Target = target;
            camera.NearPlane = 1.0;

            return camera;
        }
    }
}