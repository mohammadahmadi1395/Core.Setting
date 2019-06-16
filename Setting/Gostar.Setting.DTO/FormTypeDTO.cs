using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class FormTypeDTO : BaseDTO
    {
        public String Title { get; set; }
        public long? SubSystemID { get; set; }
        public Enums.RequestType? Enum { get; set; }
        public String PublicCode { get; set; }
        public String Coment { get; set; }

        //
        public String SubSystemTitle { get; set; }
        public String SubSystemShortName { get; set; }
    }
}
