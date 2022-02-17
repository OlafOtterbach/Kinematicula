const lineWidth = 4.0;
const backgroundColor = "#FFFFFF";
const defaultForegroundColor = "#000000"
let lock = false;

let currentBodyId;
let currentBodyIntersection;
let currentlyIsBodySelected = false;
let currentCamera;

let mouseMoved = false;
let currentMousePosition;

let canvas = document.querySelector("canvas");
canvas.addEventListener("mousedown", onMouseDown);
canvas.addEventListener("mouseup", onMouseUp);
var ctx = canvas.getContext("2d");
getScenery();

function Position(x, y) {
    this.x = x;
    this.y = y;
}

function Position3D(x, y, z) {
    this.x = x;
    this.y = y;
    this.z = z;
}

function SelectEventDto() {
    this.selectPositionX = 0.0;
    this.selectPositionY = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.camera = null;
}

function TouchEventDto() {
    this.isBodyTouched = false;
    this.bodyId = "00000000-0000-0000-0000-000000000000";
    this.touchPosition = new Position3D(0.0, 0.0, 0.0);
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.camera = null;
    this.bodyStates = null;
}

function MoveEventDto() {
    this.EventSource = "";
    this.bodyId = "00000000-0000-0000-0000-000000000000";
    this.bodyIntersection = new Position3D(0.0, 0.0, 0.0);
    this.startX = 0.0;
    this.startY = 0.0;
    this.endX = 0.0;
    this.endY = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.camera = null;
    this.bodyStates = null;
}

function ZoomEventDto() {
    this.delta = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.camera = null;
    this.bodyStates = null;
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function onMouseDown(event) {
    if (event.button === 0 || event.button === 2) {
        canvas.addEventListener("mousemove", onMouseMoved);
        canvas.addEventListener("contextmenu", onContextMenu);

        mouseMoved = false;
        currentMousePosition = getPosition(event, canvas)
        select(currentMousePosition.x, currentMousePosition.y, currentCamera);
    }
}

function onMouseUp(event) {
    if (event.button === 0) {
        canvas.removeEventListener("mousemove", onMouseMoved);
        canvas.removeEventListener("contextmenu", onContextMenu);

        if (!mouseMoved) {
            touch();
        }
        mouseMoved = false;
    }
}

function onMouseMoved(event) {
    if (event.buttons === 0) {
        canvas.removeEventListener("mousemove", onMouseMoved);
    } else {
        if (event.buttons === 1 || event.buttons === 2) {
            mouseMoved = true;
            let movedMousePosition = getPosition(event, canvas)
            if (event.buttons === 1) {
                let eventSource = event.ctrlKey ? "left mouse button and control key" : event.shiftKey ? "left mouse button and shift key" : "left mouse button"
                move(eventSource, currentBodyId, currentMousePosition, movedMousePosition);
            } else {
                zoom(currentMousePosition, movedMousePosition);
            }
        }
    }
}

function onContextMenu(event) {
    event.preventDefault();
    return false;
}

function getPosition(event, canvas) {
    let rect = canvas.getBoundingClientRect();
    return new Position(event.clientX - rect.left, event.clientY - rect.top);
}

async function getScenery() {
    lock = true;
    let url = encodeURI("http://localhost:5000/initial-graphics?canvasWidth=" + canvas.width + "&canvasHeight=" + canvas.height);
    let graphics = await fetchData(url);
    lock = false;
    drawScene(graphics);
}

async function select(x, y) {
    lock = true;
    let selectEvent = new SelectEventDto();
    selectEvent.camera = currentCamera;
    selectEvent.selectPositionX = x;
    selectEvent.selectPositionY = y;
    selectEvent.canvasWidth = canvas.width;
    selectEvent.canvasHeight = canvas.height;
    let url = encodeURI("http://localhost:5000/select");
    let bodySelection = await postData(url, selectEvent);
    currentBodyId = bodySelection.BodyId;
    currentBodyIntersection = bodySelection.BodyIntersection;
    currentlyIsBodySelected = bodySelection.IsBodyIntersected;
    lock = false;
}

async function touch() {
    lock = true;
    let touchEvent = new TouchEventDto();
    touchEvent.bodyId = currentBodyId;
    touchEvent.touchPosition = currentBodyIntersection;
    touchEvent.isBodyTouched = currentlyIsBodySelected;
    touchEvent.camera = currentCamera;
    touchEvent.canvasWidth = canvas.width;
    touchEvent.canvasHeight = canvas.height;
    let url = encodeURI("http://localhost:5000/touch");
    let sceneState = await postData(url, touchEvent);
    lock = false;
    drawScene(sceneState);
}

async function move(eventSource, bodyId, start, end) {
    sleep(50);
    if (!lock) {
        lock = true;
        let moveEvent = new MoveEventDto();
        moveEvent.EventSource = eventSource;
        moveEvent.camera = currentCamera;
        moveEvent.bodyId = bodyId;
        moveEvent.bodyIntersection = currentBodyIntersection;
        moveEvent.startX = start.x;
        moveEvent.startY = start.y;
        moveEvent.endX = end.x;
        moveEvent.endY = end.y;
        moveEvent.canvasWidth = canvas.width;
        moveEvent.canvasHeight = canvas.height;
        let url = encodeURI("http://localhost:5000/move");
        let sceneState = await postData(url, moveEvent);
        drawScene(sceneState);
        currentMousePosition = end;
        lock = false;
    }
}

async function zoom(start, end) {
    sleep(50);
    if (!lock) {
        lock = true;
        let delta = end.y - start.y;
        let zoomEvent = new ZoomEventDto();
        zoomEvent.camera = currentCamera;
        zoomEvent.delta = delta;
        zoomEvent.canvasWidth = canvas.width;
        zoomEvent.canvasHeight = canvas.height;
        let url = encodeURI("http://localhost:5000/zoom");
        let sceneState = await postData(url, zoomEvent);
        drawScene(sceneState);
        currentMousePosition = end;
        lock = false;
    }
}

function drawScene(sceneState) {
    if (sceneState !== undefined) {
        currentCamera = sceneState.Camera;
        ctx.beginPath();
        ctx.fillStyle = backgroundColor;
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.closePath();
        ctx.stroke();

        var coloredLines = sceneState.ColoredDrawLines;
        var count = coloredLines.length;
        for (idx = 0; idx < count; idx++) {
            ctx.beginPath();
            ctx.setLineDash([]);
            ctx.lineWidth = lineWidth;
            ctx.lineCap = "round";
            let color = coloredLines[idx].Color;
            ctx.strokeStyle = getColor(color);

            let lines = coloredLines[idx].DrawLines;
            var n = lines.length;
            for (i = 0; i < n; i += 4) {
                let x1 = lines[i];
                let y1 = lines[i + 1];
                let x2 = lines[i + 2];
                let y2 = lines[i + 3];
                drawLine(x1, y1, x2, y2);
            }

            ctx.closePath();
            ctx.stroke();
        }
    }
}

function drawLine(x1, y1, x2, y2) {
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
}

function getColor(colorIn)
{
    if (colorIn === 0)
        return defaultForegroundColor;

    let red   = (colorIn & 0x0F00) >> 8;
    let green = (colorIn & 0x00F0) >> 4;
    let blue = (colorIn & 0x000F);
    let colorOut = "#" + getColorValue(red) + getColorValue(green) + getColorValue(blue);
    return colorOut;
}

function getColorValue(colorIn) {
    switch (colorIn) {
        case 0:  return "00"
        case 1:  return "10";
        case 2:  return "20";
        case 3:  return "30";
        case 4:  return "40";
        case 5:  return "50";
        case 6:  return "60";
        case 7:  return "70";
        case 8:  return "80";
        case 9:  return "90";
        case 10: return "A0";
        case 11: return "B0";
        case 12: return "C0";
        case 13: return "D0";
        case 14: return "E0";
        case 15: return "FF";
        default: return "00";
    }
}

function fetchData(url) {
    let result = fetch(url)
        .then(function (response) {
            if (response.ok)
                return response.json();
            else
                throw new Error('server can has not connected');
        }).catch(function (err) {
            // Error
        });
    return result;
}

function postData(url, data) {
    let result = fetch(url, {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        redirect: "follow",
        referrer: "no-referrer",
        body: JSON.stringify(data),
    }).then(function (response) {
        if (response.ok)
            return response.json();
        else
            throw new Error('server can has not connected');
    }).catch(function (err) {
        // Error
    });
    return result;
}