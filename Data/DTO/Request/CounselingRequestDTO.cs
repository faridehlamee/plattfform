using Data.DTO.BaseDTO;
using Entites.Entities.Request;
using Entites.Entities.WorkFlow;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Request
{
    public class CounselingRequestDTO : BaseDto<CounselingRequestDTO, CounselingRequest, int>
    {
        public string EntityType { get; set; }
        public int Index { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Courses { get; set; }
        public string Description { get; set; }
        public string File { get; set; }

        public List<SelectListItem> ListCourse { get; set; }
        public string Service { get; set; }
        public string Budget { get; set; }

        //برای workflowhistory
        public bool IsConfirm { get; set; }
        public int WoIndex { get; set; }
        public string Statuse { get; set; }

        public List<WorkFlowHistory> workFlowHistories { get; set; }
    }
}
