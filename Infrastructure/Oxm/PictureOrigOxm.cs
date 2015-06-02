using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace Infrastructure.Oxm
{
    public class PictureOrigOxm : BaseMapper<PictureOrig>
    {
        public PictureOrigOxm()
            : base("picture_orig")
        {
            Text(m => m.Content).Set((m, value) => m.Content = value);
        }

        protected override PictureOrig Return()
        {
            return new PictureOrig();
        }
    }
}