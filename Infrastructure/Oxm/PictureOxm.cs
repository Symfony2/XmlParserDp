using System;
using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace Infrastructure.Oxm
{
    public class PictureOxm : BaseMapper<Picture>
    {
        public PictureOxm()
            : base("picture")
        {
            Text(m => m.Content).Set((m, value) => m.Content = value);
        }

        protected override Picture Return()
        {
            return new Picture();
        }
    }
}