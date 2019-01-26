using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using FormotsCommon.DTO;
using FormotsGUI.ViewModels;

namespace MOTS.ViewModels
{
    public class ExportClass
    {
        public string Serie { get; set; }
        public string Item { get; set; }
        public double? Nombre { get; set; }
    }

    public class BaseChartViewModel : BaseViewModel
    {
        protected ChartDto currentChartDto;
        public void ExportToExcel()
        {
            var chartTitle = currentChartDto.ChartTitle;
            var obj = new Excel.ExcelUtlity();

            var labels = currentChartDto.Labels;
            var values = currentChartDto.Values;

            var list = new List<ExportClass>();
        
            foreach (var seriesHandler in values)
            {
                var it = 0;
                var seriesHandlerCounts = seriesHandler.Counts;
                var seriesHandlerDoubleCounts = seriesHandler.DoubleCounts;
                if (seriesHandlerCounts != null)
                {
                    foreach (var seriesHandlerCount in seriesHandlerCounts)
                    {
                        list.Add(new ExportClass
                        {
                            Serie = seriesHandler.SerieTitle,
                            Item = labels.ElementAt(it),
                            Nombre = seriesHandlerCount
                        });
                        it++;
                    }
                } else if (seriesHandlerDoubleCounts != null)
                {
                    foreach (var seriesHandlerDoubleCount in seriesHandlerDoubleCounts)
                    {
                        list.Add(new ExportClass
                        {
                            Serie = seriesHandler.SerieTitle,
                            Item = labels.ElementAt(it),
                            Nombre = seriesHandlerDoubleCount
                        });
                        it++;
                    }
                }

            }

            var dt = ConvertToDataTable(list);
            var myDocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            obj.WriteDataTableToExcel(dt, "MOTS", $"{myDocumentPath}\\MOTS_Export.xlsx", chartTitle);
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    }
}
