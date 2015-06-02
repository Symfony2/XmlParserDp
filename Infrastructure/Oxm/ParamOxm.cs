using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace Infrastructure.Oxm
{
    public class ParamOxm : BaseMapper<Param>
    {
        public ParamOxm()
            : base("param")
        {
            Root().Attribute("name", m => m.Name).Set((m, value) => m.Name = value);
            Text(m => m.Content).Set((m, value) => m.Content = value);
        }

        protected override Param Return()
        {
            return new Param();
        }
    }
}