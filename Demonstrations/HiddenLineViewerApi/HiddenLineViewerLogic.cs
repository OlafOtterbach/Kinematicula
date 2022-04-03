using Kinematicula.Scening;
using Kinematicula.LogicViewing;
using Kinematicula.Mathematics;
using Kinematicula.HiddenLineGraphics;
using Kinematicula.LogicViewing.Extensions;

namespace HiddenLineViewerApi
{
    public class HiddenLineViewerLogic : IHiddenLineViewerLogic
    {
        private IHiddenLineService _hiddenLineService;
        private ILogicView _view;
        private Scene _scene;

        public HiddenLineViewerLogic(IHiddenLineService hiddenLineService, ILogicView view)
        {
            _hiddenLineService = hiddenLineService;
            _view = view;
            _scene = view.Scene;
        }

        public SceneStateDto GetScene(string cameraName, int canvasWidth, int canvasHeight)
        {
            var camera = _view.GetCamera(cameraName);
            var sceneState = new SceneStateDto()
            {
                Camera = camera.ToCameraDto(),
                ColoredDrawLines = _hiddenLineService.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToColoredLines()
            };

            return sceneState;
        }

        public SelectedBodyStateDto SelectBody(SelectEventDto selectEventDto)
        {
            var selectEvent = selectEventDto.ToSelectEvent();
            var selection = _view.SelectBody(selectEvent).ToBodySelectionDto();
            return selection;
        }

        public SceneStateDto Touch(TouchEventDto touchEventDto)
        {
            var touchEvent = touchEventDto.ToTouchEvent();
            var touchCamera = _view.Touch(touchEvent);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, touchCamera, touchEvent.CanvasWidth, touchEvent.CanvasHeight).ToColoredLines();
            var sceneState = new SceneStateDto()
            {
                Camera = touchCamera.ToCameraDto(),
                ColoredDrawLines = lines
            };

            return sceneState;
        }

        public SceneStateDto Move(MoveEventDto moveEventDto)
        {
            var moveEvent = moveEventDto.ToMoveEvent();
            var rotatedCamera = _view.Move(moveEvent);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, rotatedCamera, moveEvent.CanvasWidth, moveEvent.CanvasHeight).ToColoredLines();
            var sceneState = new SceneStateDto()
            {
                Camera = rotatedCamera.ToCameraDto(),
                ColoredDrawLines = lines
            };

            return sceneState;
        }

        public SceneStateDto Zoom(ZoomEventDto zoomEventDto)
        {
            var zoomEvent = zoomEventDto.ToZoomEvent();
            var zoomedCamera = _view.Zoom(zoomEvent);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, zoomedCamera, zoomEvent.CanvasWidth, zoomEvent.CanvasHeight).ToColoredLines();
            var sceneState = new SceneStateDto()
            {
                Camera = zoomedCamera.ToCameraDto(),
                ColoredDrawLines = lines
            };

            return sceneState;
        }
    }
}