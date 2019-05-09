using System.ComponentModel.DataAnnotations;

namespace Publishing.Domain.EntityViewModels.Categories
{
    public class ThreadCategoryViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
