//using AdminApp.API.Services;
//using AdminApp.Data;
//using CsvHelper;
//using CsvHelper.Configuration;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;

//namespace AdminApp.API.Controllers
//{
//    public class ReportsController : ApiControllerBase
//    {
//        private readonly IReportService _reportService;

//        public ReportsController(AdminAppContext db, IReportService reportService)
//            : base(db)
//        {
//            _reportService = reportService;
//        }

//        [HttpGet, Route("activationsummary")]
//        public async Task<ActionResult> GetFirmActivationSummaryReport()
//        {
//            var reportResults = _reportService.RunReport("GetFirmActivationSummaryReport");

//            return CreateFileContentResult(await CreateCSVFile(reportResults));
//        }

//        [HttpGet, Route("activeuser")]
//        public async Task<ActionResult> GetActiveUserReport()
//        {
//            var reportResults = _reportService.RunReport("GetFirmUserCountInformation");
//            return CreateFileContentResult(await CreateCSVFile(reportResults));
//        }

//        private FileContentResult CreateFileContentResult(MemoryStream fileContent)
//        {
//            using var response = new HttpResponseMessage(HttpStatusCode.OK)
//            {
//                Content = new StreamContent(fileContent)
//            };

//            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
//            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

//            var result = File(fileContent.ToArray(), "application/octet-stream");

//            return result;
//        }

//        private static async Task<MemoryStream> CreateCSVFile(IEnumerable<string[]> queryResults)
//        {
//            using var memoryStream = new MemoryStream();
//            using var streamWriter = new StreamWriter(memoryStream);
//            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
//            foreach (var result in queryResults)
//            {
//                foreach (var record in result)
//                {
//                    csvWriter.WriteField(record);
//                }
//                csvWriter.NextRecord();
//            }

//            await streamWriter.FlushAsync(); //Seems to require .Flush()?

//            return memoryStream;
//        }
//    }
//}
