using System.Collections.Generic;
using Infrastructure.Model;

namespace XmlParser.Helpers
{
    public class PictureComparer : IEqualityComparer<Picture>
    {
        public bool Equals(Picture x, Picture y)
        {
            return x.Content == y.Content;
        }

        public int GetHashCode(Picture obj)
        {
            return obj.Content.GetHashCode();
        }
    }
}