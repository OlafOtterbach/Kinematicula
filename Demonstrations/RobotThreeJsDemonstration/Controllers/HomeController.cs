﻿namespace RobotThreeJsDemonstration.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using ThreeJsViewerApi;
using ThreeJsViewerApi.EventModel;
using ThreeJsViewerApi.GraphicsModel;

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
    public ActionResult<SceneTjs> GetScene([FromQuery] int canvasWidth, [FromQuery] int canvasHeight)
    {
        var scene = _logicView.GetScene(canvasWidth, canvasHeight);

        return Ok(scene);
    }

    [HttpPost("select")]
    public ActionResult<SelectedBodyStateTjs> Select([FromBody] SelectEventTjs selectEventTjs)
    {
        var selection = _logicView.SelectBody(selectEventTjs);
        return Ok(selection);
    }

    [HttpPost("touch")]
    public ActionResult<SceneStateTjs> Touch([FromBody] TouchEventTjs touchEventTjs)
    {
        var sceneState = _logicView.Touch(touchEventTjs);
        return Ok(sceneState);
    }

    [HttpPost("move")]
    public ActionResult<SceneStateTjs> Move([FromBody] MoveEventTjs moveEventTjs)
    {
        var sceneState = _logicView.Move(moveEventTjs);
        return Ok(sceneState);
    }

    [HttpPost("zoom")]
    public ActionResult<SceneStateTjs> Zoom([FromBody] ZoomEventTjs zoomEventTjs)
    {
        var sceneState = _logicView.Zoom(zoomEventTjs);
        return Ok(sceneState);
    }

    [HttpPost("fit-in")]
    public ActionResult<SceneStateTjs> FitIn([FromBody] FitInEventTjs fitInEventTjs)
    {
        var sceneState = _logicView.FitIn(fitInEventTjs);
        return Ok(sceneState);
    }
}