﻿using Microsoft.AspNetCore.Mvc;
using CylinderDemonstration.Services;
using CylinderDemonstration.Models;

namespace CylinderDemonstration.Controllers
{
    public class HomeController : Controller
    {
        private LogicHiddenLineViewService _logicView;

        public HomeController(LogicHiddenLineViewService logicView)
        {
            _logicView = logicView;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("initial-graphics")]
        public ActionResult<SceneStateDto> GetScene([FromQuery] int canvasWidth, [FromQuery] int canvasHeight)
        {
            return Ok(_logicView.GetScene(canvasWidth, canvasHeight));
        }

        [HttpPost("select")]
        public ActionResult<SelectedBodyStateDto> Select([FromBody] SelectEventDto selectEventDto)
        {
            var selection = _logicView.SelectBody(selectEventDto);
            return Ok(selection);
        }

        [HttpPost("touch")]
        public ActionResult<SceneStateDto> Touch([FromBody] TouchEventDto touchEventDto)
        {
            var sceneState = _logicView.Touch(touchEventDto);
            return Ok(sceneState);
        }

        [HttpPost("move")]
        public ActionResult<SceneStateDto> Move([FromBody] MoveEventDto moveEventDto)
        {
            var sceneState = _logicView.Move(moveEventDto);
            return Ok(sceneState);
        }

        [HttpPost("zoom")]
        public ActionResult<SceneStateDto> Zoom([FromBody] ZoomEventDto zoomEventDto)
        {
            var sceneState = _logicView.Zoom(zoomEventDto);
            return Ok(sceneState);
        }
    }
}