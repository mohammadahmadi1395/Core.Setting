using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsahab.Setting.Common.Exceptions;
using Newtonsoft.Json;

namespace Alsahab.Setting.BL.Observers
{
    public abstract class ObserverBase
    {
        protected abstract int DoNotify(ObserverStates.ObserverStateBase stateInfo);
        internal int Notify(ObserverStates.ObserverStateBase stateInfo)
        {
            // Logger.SaveLog($"Observer = {this.GetType().FullName}\n" + $"Notify: stateInfo = {JsonConvert.SerializeObject(stateInfo)}", LogType.Info);
            int observerResult = 0;

            if (stateInfo != null)
            {
                try
                {
                    observerResult = DoNotify(stateInfo);
                }
                catch (Exception ex)
                {
                    throw new AppException(Common.Api.ApiResultStatusCode.LogicError, "Error in log registration.");
                    // Logger.SaveLog($"Error in observer {this.GetType().FullName}, state={JsonConvert.SerializeObject(stateInfo)}", LogType.Error, ex);
                }
            }
            return observerResult;
        }
    }
}
