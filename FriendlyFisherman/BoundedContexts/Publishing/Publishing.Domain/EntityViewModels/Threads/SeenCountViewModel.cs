using System;
using System.Collections.Generic;
using System.Text;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class SeenCountViewModel
    {
        public string Id { get; set; }
        public string ThreadId { get; set; }
        public string UserId { get; set; }
    }
}
