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

        public SceneStateDto GetScene(int canvasWidth, int canvasHeight)
        {
            var camera = new Camera();
            camera.NearPlane = 1.0;

            //camera.Frame = Matrix44D.CreateCoordinateSystem(
            //    new Position3D(631.83836555480957, -644.440006256103516, 518.678770065307617),
            //    new Vector3D(0.8143309354782104, 0.5803593397140503, -0.006943948566913605),
            //    new Vector3D(-0.18246249854564667, 0.26734301447868347, 0.9461687207221985));

            camera.SetCamera(0.0, 45.0, 2500.0);
            camera.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 200)) * camera.Frame;

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