using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DA
{
    public class DataAccess
    {
        public Gostar.Common.ResponseStatus ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public int? ResultCount { get; set; }
        public DataAccess()
        {
            ResponseStatus = Gostar.Common.ResponseStatus.DatabaseError;
            ErrorMessage = "Database Error\n";
        }

        protected void UseContext(Action<DA.Entities.SettingEntities> work)
        {
            using (var context = new DA.Entities.SettingEntities())
            {
                try
                {
                    work(context);
                }
                catch (Exception ex)
                {
                   string errors = "Error in DataAccess method = {0}";
                    var DBValidationErrors = ex as DbEntityValidationException;
                    if (DBValidationErrors != null && DBValidationErrors.EntityValidationErrors.Count() > 0)
                    {
                        errors += "\n" + string.Join("\n", DBValidationErrors.EntityValidationErrors.
                            Select(e =>
                            {
                                var errorStr = "";
                                if (e.ValidationErrors != null)
                                {
                                    errorStr = string.Join("\n", e.ValidationErrors.Select(ve => ve.PropertyName + ", " + ve.ErrorMessage));
                                }

                                return errorStr;
                            }));
                    }
                }

            }
        }

        protected TResult UseContext<TResult>(Func<DA.Entities.SettingEntities, TResult> work)
        {
            var result = default(TResult);
            using (var context = new DA.Entities.SettingEntities())
            {
                try
                {
                    result = work(context);
                }
                catch (Exception ex)
                {
                    string errors = "Error in DataAccess method = {0}";
                    var DBValidationErrors = ((DbEntityValidationException)ex);
                    if (DBValidationErrors != null && DBValidationErrors.EntityValidationErrors.Count() > 0)
                    {
                        errors += "\n" + string.Join("\n", DBValidationErrors.EntityValidationErrors.
                            Select(e =>
                            {
                                var errorStr = "";
                                if (e.ValidationErrors != null)
                                {
                                    errorStr = string.Join("\n", e.ValidationErrors.Select(ve => ve.PropertyName + ", " + ve.ErrorMessage));
                                }

                                return errorStr;
                            }));
                    }
                }
            }
            return result;
        }
    }
}
