using System;
using System.Web;
using System.Web.Mvc;
using DataTablesDotNet.Models;

namespace DataTablesDotNet.ModelBinding {

    public class DataTablesModelBinder : IModelBinder {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            var model = new DataTablesRequest();
            if (request.QueryString.Count > 0) {
                var requestParams = request.Params;
                var properties = model.GetType().GetProperties();

                // This will loop through the primitive properties and set the values
                foreach (var property in properties) {
                    var value = property.GetValue(model, null);
                    if (property.PropertyType.IsInterface == false) {
                        var formValue = requestParams.Get(property.Name);
                        property.SetValue(model, Convert.ChangeType(formValue, property.PropertyType), null);
                    }
                }

                // Fill lists
                for (int i = 0; i < model.iColumns; i++) {
                    model.sSearchList.Add(requestParams.Get("sSearch_" + i));
                    model.mDataProp.Add(requestParams.Get("mDataProp_" + i));
                    model.bRegexList.Add(Convert.ToBoolean(requestParams.Get("bRegex_" + i) ?? "false"));
                    model.bSearchable.Add(Convert.ToBoolean(requestParams.Get("bSearchable_" + i) ?? "false"));
                    model.bSortable.Add(Convert.ToBoolean(requestParams.Get("bSortable_" + i) ?? "false"));
                }

                // Use iSortingCols to fill sort-related lists
                for (int i = 0; i < model.iSortingCols; i++) {
                    model.iSortCol.Add(requestParams.Get("iSortCol_" + i));
                    model.sSortDir.Add(requestParams.Get("sSortDir_" + i));
                }
            }

            return model;
        }
    }
}