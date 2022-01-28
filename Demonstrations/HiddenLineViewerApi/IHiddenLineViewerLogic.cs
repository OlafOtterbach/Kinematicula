namespace HiddenLineViewerApi
{
    public interface IHiddenLineViewerLogic
    {
        SceneStateDto GetScene(int canvasWidth, int canvasHeight);
        SelectedBodyStateDto SelectBody(SelectEventDto selectEventDto);
        SceneStateDto Touch(TouchEventDto touchEventDto);
        SceneStateDto Move(MoveEventDto moveEventDto);
        SceneStateDto Zoom(ZoomEventDto zoomEventDto);
    }
}
