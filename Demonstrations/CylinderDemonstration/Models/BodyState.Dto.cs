using System;

namespace CylinderDemonstration.Models
{
    public class BodyStateDto
    {
        public Guid BodyId { get; set; }

        public CardanFrameDto Frame { get; set; }
    }
}
