﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsahab.Common;
using Alsahab.Common.Exceptions;
using Newtonsoft.Json;

namespace Alsahab.Setting.BL.Log
{
    public abstract class ObserverBase<TDto> where TDto : BaseDTO
    {
        protected abstract int DoNotify(ObserverStateBase<TDto> stateInfo);
        internal int Notify(ObserverStateBase<TDto> stateInfo)
        {
            // Logger.SaveLog($"Observer = {this.GetType().FullName}\n" + $"Notify: stateInfo = {JsonConvert.SerializeObject(stateInfo)}", LogType.Info);
            int observerResult = 0;

            if (stateInfo != null)
            {
                try
                {
                    observerResult = DoNotify(stateInfo);
                }
                catch// (Exception ex)
                {
                    throw new AppException(ResponseStatus.ServerError, "Error in log registration.");
                    // Logger.SaveLog($"Error in observer {this.GetType().FullName}, state={JsonConvert.SerializeObject(stateInfo)}", LogType.Error, ex);
                }
            }
            return observerResult;
        }
    }
}
