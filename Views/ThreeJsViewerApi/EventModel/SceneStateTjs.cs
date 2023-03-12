using ThreeJsViewerApi.GraphicsModel;

namespace ThreeJsViewerApi.EventModel;

public record struct BodyItemTjs(Guid Id, FrameTjs Frame);

public record struct SceneStateTjs(CameraTjs Camera, BodyItemTjs[] GraphicsState);

