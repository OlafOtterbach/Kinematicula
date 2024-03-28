//////////////////////////////////////////////////////////////////////
//
// types
//
//////////////////////////////////////////////////////////////////////

function Position2D(x, y) {
    this.x = x;
    this.y = y;
}

function Position3D(x, y, z) {
    this.x = x;
    this.y = y;
    this.z = z;
}

function SelectEventTjs() {
    this.selectPositionX = 0.0;
    this.selectPositionY = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.cameraId = "00000000-0000-0000-0000-000000000000";
}

function TouchEventTjs() {
    this.isBodyTouched = false;
    this.bodyId = "00000000-0000-0000-0000-000000000000";
    this.touchPosition = new Position3D(0.0, 0.0, 0.0);
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.cameraId = "00000000-0000-0000-0000-000000000000";
    this.bodyStates = null;
}

function MoveEventTjs() {
    this.EventSource = "";
    this.bodyId = "00000000-0000-0000-0000-000000000000";
    this.bodyIntersection = new Position3D(0.0, 0.0, 0.0);
    this.startX = 0.0;
    this.startY = 0.0;
    this.endX = 0.0;
    this.endY = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.cameraId = "00000000-0000-0000-0000-000000000000";
    this.bodyStates = null;
}

function ZoomEventTjs() {
    this.delta = 0.0;
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.cameraId = "00000000-0000-0000-0000-000000000000";
    this.bodyStates = null;
}

function FitInEventTjs() {
    this.canvasWidth = 0;
    this.canvasHeight = 0;
    this.cameraId = "00000000-0000-0000-0000-000000000000";
}

function CameraItem(id, name, cameraTjs) {
    this.id = id;
    this.name = name;
    this.cameraTjs = cameraTjs
}




//////////////////////////////////////////////////////////////////////
//
// variables
//
//////////////////////////////////////////////////////////////////////

// rendering
let canvas;
let rendererTjs;
let sceneTjs;
let idAndBodyTjsDict = new Object();
let idAndNameAndCameraTjs = [];

// current state
let currentCameraTjs;
let currentCameraLightTjs;
let currentBodyId;
let currentBodyIntersection;
let currentlyIsBodySelected = false;


// mouse interaction
let mouseMoved = false;
let currentMousePosition;



//////////////////////////////////////////////////////////////////////
//
// program
//
//////////////////////////////////////////////////////////////////////
init();

// end



async function init() {
    initCanvasAndRenderer();

    let sceneSrv = await getSceneFromServer();
    let backgroundColor = sceneSrv.Background;

    createSceneTjs(backgroundColor);
    addBodiesToSceneTjs(sceneSrv.Bodies);
    getCamerasTjs(sceneSrv.Cameras);
    setCurrentCamera("CameraOne");
    animate();
}

function animate() {
    requestAnimationFrame(animate);

    currentCameraLightTjs.position.copy(currentCameraTjs.cameraTjs.position);
    currentCameraLightTjs.rotation.copy(currentCameraTjs.cameraTjs.rotation);

    rendererTjs.render(sceneTjs, currentCameraTjs.cameraTjs);
};




function initCanvasAndRenderer() {
    canvas = document.getElementById("MyCanvas");
    canvas.addEventListener("mousedown", onMouseDown);
    canvas.addEventListener("mouseup", onMouseUp);

    rendererTjs = new THREE.WebGLRenderer({
        antialias: true,
        canvas: canvas
    });
}


function createSceneTjs(backgroundColor) {
    sceneTjs = new THREE.Scene();

    // ambient light
    const ambientLight = new THREE.AmbientLight(0xffffff, 0.2); // soft white light
    sceneTjs.add(ambientLight);
    sceneTjs.background = new THREE.Color(backgroundColor);

    // direction light for abuff
    var lamp = new THREE.DirectionalLight(0xffffff, 1);
    lamp.position.set(0, 0, 10000);
    lamp.rotation.set(new THREE.Euler(0, 0, 0, 'XYZ'));
    lamp.castShadow = true;
    sceneTjs.add(lamp);

    var light = new THREE.DirectionalLight(0xffffff, 1);
    light.position.set(0, 0, 0);
    light.rotation.set(new THREE.Euler(0, 0, 0, 'XYZ'));
    light.castShadow = true;
    currentCameraLightTjs = new THREE.Object3D();
    currentCameraLightTjs.add(light);
    sceneTjs.add(currentCameraLightTjs);
}


async function getSceneFromServer() {
    lock = true;

    let width = canvas.width;
    let height = canvas.height;//?order_id=1

    let url = encodeURI("http://localhost:5000/scene?canvasWidth=" + width + "&&canvasHeight=" + height);
    let scene = await fetchData(url);
    lock = false;

    return scene;
}


function addBodiesToSceneTjs(bodies) {

    function convertBodyToBodyTjs(body) {
        const positions = [];
        const normals = [];
        const colors = [];
        for (const vertex of body.Vertices) {
            positions.push(vertex.Position.X);
            positions.push(vertex.Position.Y);
            positions.push(vertex.Position.Z);
            normals.push(vertex.Normal.Vx);
            normals.push(vertex.Normal.Vy);
            normals.push(vertex.Normal.Vz);
            colors.push(vertex.Color.Red);
            colors.push(vertex.Color.Green);
            colors.push(vertex.Color.Blue);
        }

        const indices = body.Indices;

        const geometry = new THREE.BufferGeometry();
        geometry.setAttribute('position', new THREE.BufferAttribute(new Float32Array(positions), 3));
        geometry.setAttribute('normal', new THREE.BufferAttribute(new Float32Array(normals), 3));
        geometry.setAttribute('color', new THREE.BufferAttribute(new Float32Array(colors), 3));
        geometry.setIndex(new THREE.BufferAttribute(new Uint16Array(indices), 1));
        var material = new THREE.MeshPhongMaterial({ vertexColors: true });
        let bodyTjs = new THREE.Mesh(geometry, material);

        addEdgesToMesh(body, bodyTjs);

        bodyTjs.matrixAutoUpdate = false;
        const frame = body.Frame;
        setBodyFrame(bodyTjs, frame);

        return bodyTjs;
    }

    for (let body of bodies) {
        let bodyTjs = convertBodyToBodyTjs(body);
        idAndBodyTjsDict[body.Id] = bodyTjs;
        sceneTjs.add(bodyTjs);
    }
}


function addEdgesToMesh(body, bodyTjs) {
    let vertices = [];
    for (const point of body.EdgePoints) {
        vertices.push(new THREE.Vector3(point.X, point.Y, point.Z));
    }

    let indices = [];
    for (const index of body.EdgeIndices) {
        indices.push(index);
    }

    let edgeColor = body.EdgeColor;
    var materialLine = new THREE.LineBasicMaterial({ color: edgeColor });
    var geometryLine = new THREE.BufferGeometry().setFromPoints(vertices);
    geometryLine.setIndex(new THREE.BufferAttribute(new Uint16Array(indices), 1));
    
    var line = new THREE.LineSegments(geometryLine, materialLine);
    bodyTjs.add(line);
}

function getCamerasTjs(cameras) {
    for (let i = 0; i < cameras.length; i++) {
        var camera = cameras[i];
        const euler = camera.EulerFrame;

        const cameraTjs = new THREE.PerspectiveCamera(camera.FrustumInDegree, canvas.width / canvas.height, 1, 10000);
        setCameraFrame(cameraTjs, euler);

        const cameraItem = new CameraItem(camera.Id, camera.Name, cameraTjs);
        idAndNameAndCameraTjs.push(cameraItem);
   }
}


function setCurrentCamera(name) {
    const cameraTjs = idAndNameAndCameraTjs.find(function (item) { return item.name === name });
    if (cameraTjs != null) {
        currentCameraTjs = cameraTjs;
    }
}


function onMouseDown(event) {
    if (event.button === 0 || event.button === 2) {
        canvas.addEventListener("mousemove", onMouseMoved);
        canvas.addEventListener("contextmenu", onContextMenu);

        mouseMoved = false;
        currentMousePosition = getPosition(event, canvas)
        select(currentMousePosition.x, currentMousePosition.y);
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

function onFitIn() {
    fitIn();
}

function onContextMenu(event) {
    event.preventDefault();
    return false;
}

function getPosition(event, canvas) {
    let rect = canvas.getBoundingClientRect();
    return new Position2D(event.clientX - rect.left, event.clientY - rect.top);
}


async function select(x, y) {
    lock = true;

    let selectEvent = new SelectEventTjs();
    selectEvent.cameraId = currentCameraTjs.id;
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
    let touchEvent = new TouchEventTjs();
    touchEvent.bodyId = currentBodyId;
    touchEvent.touchPosition = currentBodyIntersection;
    touchEvent.isBodyTouched = currentlyIsBodySelected;
    touchEvent.cameraId = currentCameraTjs.id;
    touchEvent.canvasWidth = canvas.width;
    touchEvent.canvasHeight = canvas.height;
    let url = encodeURI("http://localhost:5000/touch");
    let sceneState = await postData(url, touchEvent);
    lock = false;
    UpdateScene(sceneState);
}

async function move(eventSource, bodyId, start, end) {
    sleep(50);
    if (!lock) {
        lock = true;

        let moveEvent = new MoveEventTjs();
        moveEvent.EventSource = eventSource;
        moveEvent.cameraId = currentCameraTjs.id;
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
        UpdateScene(sceneState);
        currentMousePosition = end;

        lock = false;
    }
}

async function zoom(start, end) {
    sleep(50);
    if (!lock) {
        lock = true;
        let delta = end.y - start.y;
        let zoomEvent = new ZoomEventTjs();
        zoomEvent.cameraId = currentCameraTjs.id;
        zoomEvent.delta = delta * 4.0;
        zoomEvent.canvasWidth = canvas.width;
        zoomEvent.canvasHeight = canvas.height;
        let url = encodeURI("http://localhost:5000/zoom");
        let sceneState = await postData(url, zoomEvent);
        UpdateScene(sceneState);
        currentMousePosition = end;
        lock = false;
    }
}

async function fitIn() {
    if (!lock) {
        lock = true;
        let fitInEvent = new FitInEventTjs();
        fitInEvent.cameraId = currentCameraTjs.id;
        fitInEvent.canvasWidth = canvas.width;
        fitInEvent.canvasHeight = canvas.height;
        let url = encodeURI("http://localhost:5000/fit-in");
        let sceneState = await postData(url, fitInEvent);
        UpdateScene(sceneState);
        lock = false;
    }
}

function UpdateScene(sceneState) {
    for (let i = 0; i < sceneState.GraphicsState.length; i++) {
        let item = sceneState.GraphicsState[i];
        setBodyFrame(idAndBodyTjsDict[item.Id], item.Frame);
    }

    var cameraId = sceneState.Camera.Id;
    const camera = idAndNameAndCameraTjs.find(function (item) { return item.id === cameraId });
    setCameraFrame(camera.cameraTjs, sceneState.Camera.EulerFrame);
}


function setBodyFrame(bodyTjs, frame) {
    bodyTjs.matrix.set(
        frame.A11, frame.A12, frame.A13, frame.A14,
        frame.A21, frame.A22, frame.A23, frame.A24,
        frame.A31, frame.A32, frame.A33, frame.A34,
        frame.A41, frame.A42, frame.A43, frame.A44);
}

function setCameraFrame(cameraTjs, eulerFrame) {
    cameraTjs.position.x = eulerFrame.X;
    cameraTjs.position.y = eulerFrame.Y;
    cameraTjs.position.z = eulerFrame.Z;
    cameraTjs.rotation.x = eulerFrame.AngleX;
    cameraTjs.rotation.y = eulerFrame.AngleY;
    cameraTjs.rotation.z = eulerFrame.AngleZ;
}


function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
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


