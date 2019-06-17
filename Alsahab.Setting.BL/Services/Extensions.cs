using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;
using System.Globalization;
using System.Threading;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.BL
{
    public static class Extensions
    {
        public static TResponse CallBL<TResponse, T>(this T bl, Func<T, TResponse> work, UserInfoDTO User,PagingInfoDTO paging = null) where T : BaseBusiness//, Language Language)
        {
            TResponse response = default(TResponse);
            try
            {
                bl.PagingInfo = paging;
                bl.User = User;
                response = work(bl);
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public static TResponse CallBL<TResponse, T>(this T bl, Func<T, TResponse> work, UserInfoDTO User, Language? Language, PagingInfoDTO paging = null) where T : BaseBusiness
        {
            TResponse response = default(TResponse);

            try
            {
                bl.PagingInfo = paging;
                bl.User = User;
                if (Language.HasValue)
                    switch (Language.Value)
                    {
                        case Alsahab.Common.Language.ar_IQ:
                            bl.Culture = new CultureInfo("ar");
                            break;
                        case Alsahab.Common.Language.fa_IR:
                            bl.Culture = new CultureInfo("fa");
                            break;
                        case Alsahab.Common.Language.en_US:
                            bl.Culture = new CultureInfo("en");
                            break;
                        default:
                            bl.Culture = new CultureInfo("en");
                            break;
                    }
                else
                    bl.Culture = new CultureInfo("en");

                Thread.CurrentThread.CurrentUICulture = bl.Culture;
                response = work(bl);
            }
            catch (Exception ex)
            {

            }
            return response;

        }
    }
}
