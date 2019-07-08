using System;
using System.ServiceModel;
using Alyatim.Member.SC;
using UserManagement.SC;
using Alsahab.Common.Exceptions;

namespace Alsahab.Setting.BL
{
    public static class ServiceUtility
    {
        public static T CallUserManagement<T>(Func<IUserManagementService, T> work)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri("http://192.168.1.7:1011/UserManagementService.svc"));
            var client = new ChannelFactory<IUserManagementService>(binding, endpoint);
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
            }
        }
        public static T CallMember<T>(Func<IMemberService, T> work)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri("http://192.168.1.7:1021/MemberService.svc"));
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
            catch // (Exception ex)
            {
                client.Abort();
                throw new AppException(Common.ResponseStatus.ServerError, "Error in connect to member service.");
            }
        }
    }
}
