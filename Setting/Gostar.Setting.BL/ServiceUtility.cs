using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using UserManagement.SC;
using Alyatim.Member.SC;
//using Gostar.Setting.SC;

namespace Gostar.Setting.BL
{
    public static class ServiceUtility
    {
        //public static T Call<T>(Func<ISettingService, T> work)
        //{
        //    var client = new ChannelFactory<ISettingService>("SettingService");
        //    if (client == null)
        //        return default(T);
        //    try
        //    {
        //        T result;
        //        result = work(client.CreateChannel());
        //        client.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        client.Abort();
        //        //FormAction.ShowMessage("خطا در ارتباط با سرور", MessageBoxButtons.OK, MessageType.Error);
        //        return default(T);
        //    }
        //}
        public static T CallUserManagement<T>(Func<IUserManagementService, T> work)
        {
            var client = new ChannelFactory<IUserManagementService>("UserManagementService");
            if (client == null)
                return default(T);
            try
            {
                T result;
                result = work(client.CreateChannel());
                client.Close();
                return result;
            }
            catch (Exception ex)
            {
                client.Abort();
                //FormAction.ShowMessage("خطا در ارتباط با سرور", MessageBoxButtons.OK, MessageType.Error);
                return default(T);
            }
        }
        public static T CallMember<T>(Func<IMemberService, T> work)
        {
            var client = new ChannelFactory<IMemberService>("MemberService");
            if (client == null)
                return default(T);
            try
            {
                T result;
                result = work(client.CreateChannel());
                client.Close();
                return result;
            }
            catch (Exception ex)
            {
                client.Abort();
                //FormAction.ShowMessage("خطا در ارتباط با سرور", MessageBoxButtons.OK, MessageType.Error);
                return default(T);
            }
        }
    }
}
