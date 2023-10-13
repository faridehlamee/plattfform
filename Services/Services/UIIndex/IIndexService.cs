using Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UIIndex
{
    public interface IIndexService
    {
        Task<IndexViewModel> GetData();


    }
}
