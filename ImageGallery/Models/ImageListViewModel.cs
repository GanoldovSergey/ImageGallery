using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageGallery.Models
{
    public class ImageListViewModel
    {
        public IEnumerable<string> ImageNames { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}