using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.WebFramework.Api;

namespace Alsahab.Setting.MyAPI.Models
{
    public class GeneratedFormDTO : BaseDTO<GeneratedFormDTO, GeneratedForm>
    {
        public String PublicCode { get; set; }
        public String PrivateCode { get; set; }
        public long? SubSystemID { get; set; }
        public long? UniqeCode { get; set; }
       
    }
}
