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
                var qs = request.QueryString;
                var properties = model.GetType().GetProperties();

                // This will loop through the primitive properties and set the values
                foreach (var property in properties) {
                    var value = property.GetValue(model, null);
                    if (property.PropertyType.IsInterface == false) {
                        var formValue = qs.Get(property.Name);
                        property.SetValue(model, Convert.ChangeType(formValue, property.PropertyType), null);
                    }
                }

                // Fill lists
                for (int i = 0; i < model.iColumns; i++) {
                    model.sSearchList.Add(qs.Get("sSearch_" + i));
                }

                for (int i = 0; i < model.iColumns; i++) {
                    model.mDataProp.Add(qs.Get("mDataProp_" + i));
                }

                for (int i = 0; i < model.iColumns; i++) {
                    model.bRegexList.Add(Convert.ToBoolean(qs.Get("bRegex_" + i) ?? "false"));
                }

                for (int i = 0; i < model.iColumns; i++) {
                    model.bSearchable.Add(Convert.ToBoolean(qs.Get("bSearchable_" + i) ?? "false"));
                }

                for (int i = 0; i < model.iColumns; i++) {
                    model.bSortable.Add(Convert.ToBoolean(qs.Get("bSortable_" + i) ?? "false"));
                }

                for (int i = 0; i < model.iSortingCols; i++) {
                    model.iSortCol.Add(qs.Get("iSortCol_" + i));
                    model.sSortDir.Add(qs.Get("sSortDir_" + i));
                }
            }

            return model;
        }
    }
}