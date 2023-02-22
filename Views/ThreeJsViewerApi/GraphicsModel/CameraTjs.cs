namespace ThreeJsViewerApi.GraphicsModel;

public record CameraTjs(
    string Name,
    Guid Id,
    double frustumInDegree,
    EulerFrameTjs EulerFrame);
