using Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashBoard.Models
{
    public class Dashboard
    {

        public List<ReportModelView> SaleReport { get; set; }

        
        public ReportModelView AllPriceReport { get; set; }
        public ReportModelView NewOrderReport { get; set; }
        public ReportModelView AllOrderReport { get; set; }
        public ReportModelView RegisterUserReport { get; set; }


    }
}
