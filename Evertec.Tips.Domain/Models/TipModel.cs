using Evertec.Tips.Domain.Enumerations;
using Evertec.Tips.Mobile.Domain.Entities;

namespace Evertec.Tips.Domain.Models
{
    public class TipModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int AuthorId { get; set; }


        public Response Validate()
        {
            if (AuthorId == 0)
                return new (Validations.RequiredAuthor.ToString(), false);

            if (string.IsNullOrEmpty(Title))
                return new (Validations.RequiredTitle.ToString(), false);

            return new ();
        }
    }
}
