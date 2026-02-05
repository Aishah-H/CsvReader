using CsvHelper;
using CsvHelper.TypeConversion;
using CsvReaderApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
namespace CsvReaderApp.Services
{
    public class CsvService
    {
        public IEnumerable<User> ReadCsvFile(Stream fileStream)
        {
            try
            {
                using (var reader = new StreamReader (fileStream))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<User>();
                    return records.ToList();
                }
            }

            catch (HeaderValidationException ex)
            {
                throw new ApplicationException("CSV file is invald.", ex);
            }
            catch (TypeConverterException ex)
            {
                throw new ApplicationException("CSV file contains invalid data format.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error reading CSV file", ex);
            }
        }
    }
}
