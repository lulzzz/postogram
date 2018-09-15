using System;

namespace Postogram
{
    public class Content
    {
        public Guid Id { get; set; }
        public bool Posted { get; set; }
        public string Description { get; set; }
        public Picture[] Pictures { get; set; }
    }
}
