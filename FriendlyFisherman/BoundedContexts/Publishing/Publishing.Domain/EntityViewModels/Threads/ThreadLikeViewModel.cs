using System;
using System.Collections.Generic;
using System.Text;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class ThreadLikeViewModel
    {
        public LikeViewModel UserLike { get; set; }
        public Dictionary<int?, int> Likes { get; set; }
    }
}
