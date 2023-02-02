namespace HiddenLineViewerApi;

using Kinematicula.Graphics;

public static class CameraExtensions
{
    public static CameraDto ToCameraDto(this Camera camera)
    {
        var cameraDto = new CameraDto()
        {
            Id = camera.Id,
            Name = camera.Name
        };

        return cameraDto;
    }
}