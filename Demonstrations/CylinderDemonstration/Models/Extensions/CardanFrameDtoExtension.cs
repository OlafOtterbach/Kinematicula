using Kinematicula.Mathematics;

namespace CylinderDemonstration.Models.Extensions
{
    public static class CardanFrameDtoExtension
    {
        public static CardanFrame ToCardanFrame(this CardanFrameDto cardanFrameDto)
        {
            var cardanFrame = new CardanFrame(
                new Position3D(cardanFrameDto.OffsetX, cardanFrameDto.OffsetY, cardanFrameDto.OffsetZ),
                cardanFrameDto.AlphaAngleAxisX,
                cardanFrameDto.BetaAngleAxisY,
                cardanFrameDto.GammaAngleAxisZ);

            return cardanFrame;
        }
    }

}
