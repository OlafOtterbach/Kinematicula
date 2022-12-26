namespace DoubleRobotDemonstration.Controllers;

using Microsoft.AspNetCore.Mvc;
using HiddenLineViewerApi;
using ThreeJsViewerApi;
using ThreeJsViewerApi.Model;

public class HomeController : Controller
{
    private IThreeJsViewerLogic _logicView;

    public HomeController(IThreeJsViewerLogic logicView)
    {
        _logicView = logicView;
    }

    // GET: HomeController
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet("scene")]
    public ActionResult<SceneTjs> GetScene()
    {
        var scene = _logicView.GetScene();

        return Ok(scene);
    }

    //[HttpPost("select")]
    //public ActionResult<SelectedBodyStateDto> Select([FromBody] SelectEventDto selectEventDto)
    //{
    //    var selection = _logicView.SelectBody(selectEventDto);
    //    return Ok(selection);
    //}

    //[HttpPost("touch")]
    //public ActionResult<SceneStateDto> Touch([FromBody] TouchEventDto touchEventDto)
    //{
    //    var sceneState = _logicView.Touch(touchEventDto);
    //    return Ok(sceneState);
    //}

    //[HttpPost("move")]
    //public ActionResult<SceneStateDto> Move([FromBody] MoveEventDto moveEventDto)
    //{
    //    var sceneState = _logicView.Move(moveEventDto);
    //    return Ok(sceneState);
    //}

    //[HttpPost("zoom")]
    //public ActionResult<SceneStateDto> Zoom([FromBody] ZoomEventDto zoomEventDto)
    //{
    //    var sceneState = _logicView.Zoom(zoomEventDto);
    //    return Ok(sceneState);
    //}
}