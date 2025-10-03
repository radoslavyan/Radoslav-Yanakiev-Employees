using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PairOfEmployees.Application.Contracts;
using PairOfEmployees.Domain.Models;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace PairOfEmployees.Persistence.Loader
{
    public class CsvLoader : IPairOfEmployeesLoader
    {
        public static readonly string[] SupportedDateFormats = new[] 
        {
            "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yyyy", "MM-dd-yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "yyyy.MM.dd",
            "yyyyMMdd", "ddMMyyyy", "MMMyyyy", "MMM dd, yyyy", "MMMM dd, yyyy", "dd MMM yyyy", "dd MMMM yyyy"

        };
        public async Task<IReadOnlyList<PairOfEmployeesData>> ImportAsync(string filePath, CancellationToken cancellation = default)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                BadDataFound = null,        
                MissingFieldFound = null,   
                DetectDelimiter = false,    
                Delimiter = ","
            };

            var record = new List<PairOfEmployeesData>();
            using var fileReader = new StreamReader(filePath);
            using var csv = new CsvReader(fileReader, config);

            if (!await csv.ReadAsync()) return record; 
            csv.ReadHeader();

            if (csv.HeaderRecord != null)
            {
                for (int i = 0; i < csv.HeaderRecord.Length; i++)
                {
                    csv.HeaderRecord[i] = csv.HeaderRecord[i]?.Trim();
                }
            }

            while (await csv.ReadAsync()) 
            {
                cancellation.ThrowIfCancellationRequested();

                var empId = csv.GetField<int>(0);
                var projectId = csv.GetField<int>(1);
                var dateFromStr = csv.GetField<string>(2);
                var dateToStr = csv.GetField<string>(3);

                var from = string.IsNullOrWhiteSpace(dateFromStr)
                    ? throw new FormatException("DateFrom field is null or empty.")
                    : DateTime.ParseExact(dateFromStr, SupportedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                var to = string.IsNullOrWhiteSpace(dateToStr) || dateToStr.Equals("NULL", StringComparison.OrdinalIgnoreCase) ?
                    DateTime.Now :
                    DateTime.ParseExact(dateToStr, SupportedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                record.Add(new PairOfEmployeesData(
                    empId,
                    projectId,
                    filePath, 
                    DateOnly.FromDateTime(from),
                    DateOnly.FromDateTime(to)
                ));

            }


            return record;

        }

        public DateOnly ParseDateValidation(string dateStr)
        {
            dateStr = dateStr.Trim();
            if (string.Equals(dateStr, "Null", StringComparison.OrdinalIgnoreCase))
                return DateOnly.FromDateTime(DateTime.Today);

            if (DateOnly.TryParseExact(dateStr, SupportedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            if (DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return DateOnly.FromDateTime(dt);

            throw new FormatException($"Invalid date format: {dateStr}");
        }
    }
}
