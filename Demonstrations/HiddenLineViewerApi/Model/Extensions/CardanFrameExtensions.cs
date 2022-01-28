using Kinematicula.Mathematics;

namespace HiddenLineViewerApi
{
    public static class CardanFrameExtensions
    {
        public static CardanFrameDto ToCardanFrameDto(this CardanFrame cardanFrame)
        {
            var cardanFrameDto = new CardanFrameDto()
            {
                OffsetX = cardanFrame.Offset.X,
                OffsetY = cardanFrame.Offset.Y,
                OffsetZ = cardanFrame.Offset.Z,
                AlphaAngleAxisX = cardanFrame.AlphaAngleAxisX,
                BetaAngleAxisY = cardanFrame.BetaAngleAxisY,
                GammaAngleAxisZ = cardanFrame.GammaAngleAxisZ
            };

            return cardanFrameDto;
        }

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
