using System;
using System.Collections.Generic;

namespace NPhotos.Models
{
    public class albums
    {
        public string id { get; set; }
        public string title { get; set; }
        public string productUrl { get; set; }
        public string totalMediaItems { get; set; }
        public string coverPhotoBaseUrl { get; set; }
    }

    public class RootObject<T>
    {
        public List<T> albums { get; set; }
    }
}
