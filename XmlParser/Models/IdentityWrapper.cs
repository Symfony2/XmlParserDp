using System.Collections.Generic;
using System.Linq;
using Infrastructure.Model;

namespace XmlParser.Models
{
    public class IdentityWrapper
    {
        private readonly IEnumerable<ExcelMatchModel> excelMatchModels;
        private readonly IEnumerable<JsonMatchModel> jsonMatchModels;
        private Dictionary<string, string> pars;

        public IdentityWrapper(IEnumerable<ExcelMatchModel> excelMatchModels, IEnumerable<JsonMatchModel> jsonMatchModels)
        {
            this.excelMatchModels = excelMatchModels;
            this.jsonMatchModels = jsonMatchModels;
            pars = new Dictionary<string, string>();
        }

        public void Initialize()
        {
            var jsonModels = jsonMatchModels
                .Where(j => !string.IsNullOrEmpty(j.AdmitadCategoryId) && !string.IsNullOrEmpty(j.OriginalId))
                .Select(j => new {j.AdmitadCategoryId, j.OriginalId });

            var query = from em in excelMatchModels
                        join jm in jsonModels on em.OldCatId equals jm.OriginalId
                        select new KeyValuePair<string, string>(em.NewCatId, jm.AdmitadCategoryId);

            foreach (KeyValuePair<string, string> pair in query)
                pars.Add(pair.Key, pair.Value);
        }

        public string this[string newCategoryId]
        {
            get
            {
                if(pars.ContainsKey(newCategoryId))
                    return pars[newCategoryId];
                return null;
            }
        }
    }
}