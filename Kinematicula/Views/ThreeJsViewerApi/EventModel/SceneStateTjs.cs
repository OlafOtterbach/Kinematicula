namespace Kinematicula.Views.ThreeJsViewerApi.EventModel;

using Kinematicula.Views.ThreeJsViewerApi.GraphicsModel;

public record struct BodyItemTjs(Guid Id, FrameTjs Frame);

public record struct SceneStateTjs(CameraTjs Camera, BodyItemTjs[] GraphicsState);

