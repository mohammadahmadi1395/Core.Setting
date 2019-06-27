using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Alyatim.Member.SC;
using Alyatim.Member.DTO;
using UserManagement.SC;
using UserManagement.DTO;
using Alsahab.Common.Exceptions;

namespace Alsahab.Setting.BL
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
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri("http://192.168.1.7:1011/UserManagementService.svc"));
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
            catch //(Exception ex)
            {
                client.Abort();
                throw new AppException(Common.ResponseStatus.ServerError, "Error in connect to user management service.");
                //FormAction.ShowMessage("خطا در ارتباط با سرور", MessageBoxButtons.OK, MessageType.Error);
                return default(T);
            }
        }
        public static T CallMember<T>(Func<IMemberService, T> work)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri("http://192.168.1.7:1021/MemberService.svc"));
            // var channelFactory = new ChannelFactory<IUserManagementService>(binding, endpoint);
            // var serviceClient = channelFactory.CreateChannel();
            // var result = serviceClient.Ping("Ping");
            // channelFactory.Close();
            var client = new ChannelFactory<IMemberService>(binding, endpoint);
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
                throw new AppException(Common.ResponseStatus.ServerError, "Error in connect to member service.");
                //FormAction.ShowMessage("خطا در ارتباط با سرور", MessageBoxButtons.OK, MessageType.Error);
                // return default(T);
            }
        }
    }
}
