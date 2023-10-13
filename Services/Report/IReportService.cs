
using Data.DTO;
using Entites.Entities.User;
using Services.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Report
{
    public interface IReportService
    {
        Task<List<ReportModelView>> SaleOrderReport();
        Task<ReportModelView> AllPriceReport();
        Task<ReportModelView> NewOrderReport();
        Task<ReportModelView> AllOrderReport();
        Task<ReportModelView> RegisterReport();

    }
}