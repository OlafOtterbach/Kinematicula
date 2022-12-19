namespace HiddenLineViewerApi
{
    public class CameraDto
    {
        public string Name { get; set; }

        public double NearPlane { get; set; }

        public double Distance { get; set; }

        public PositionDto Target { get; set; }

        public CardanFrameDto Frame { get; set; }
    }
}