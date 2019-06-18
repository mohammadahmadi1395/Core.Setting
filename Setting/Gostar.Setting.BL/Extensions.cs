using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;
using System.Globalization;
using System.Threading;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL
{
    public static class Extensions
    {
        public static TResponse CallBL<TResponse, T>(this T bl, Func<T, TResponse> work, UserInfoDTO User,PagingInfoDTO paging = null) where T : BaseBL//, Language Language)
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
        public static TResponse CallBL<TResponse, T>(this T bl, Func<T, TResponse> work, UserInfoDTO User, Language? Language, PagingInfoDTO paging = null) where T : BaseBL
        {
            TResponse response = default(TResponse);

            try
            {
                bl.PagingInfo = paging;
                bl.User = User;
                if (Language.HasValue)
                    switch (Language.Value)
                    {
                        case  Common.Language.ar_IQ:
                            bl.Culture = new CultureInfo("ar");
                            break;
                        case Common.Language.fa_IR:
                            bl.Culture = new CultureInfo("fa");
                            break;
                        case Common.Language.en_US:
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
