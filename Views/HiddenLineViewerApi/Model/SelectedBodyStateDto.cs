﻿namespace HiddenLineViewerApi
{
    public class SelectedBodyStateDto
    {
        public bool IsBodyIntersected { get; set; }

        public Guid BodyId { get; set; }

        public PositionDto BodyIntersection { get; set; }
    }
}
