namespace ThreeJsViewerApi.GraphicsModel;
public record struct SceneStateTjs(CameraTjs Camera,  Dictionary<Guid, FrameTjs> GraphicsState);

