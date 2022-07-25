using Evertec.Tips.Domain.Entities;

namespace Evertec.Tips.Domain.Dto
{
    public class MessageItemDto
    {
        public TipEntity Message { get; set; }

        public int SourceId { get; set; }

        public int TargetId { get; set; }
    }
}
