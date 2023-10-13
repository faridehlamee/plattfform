using Data.DTO.BaseDTO;
using Entites.Entities.User;
using System;
using static Common.AllEnum.Commons;

namespace Data.DTO.User
{
    public class IdentificationCodeRuleDTO : BaseDto<IdentificationCodeRuleDTO, IdentificationCodeRule, int>
    {
        public bool ForOwner { get; set; }
        public bool ForUser { get; set; }
        public bool ForHasDisCount { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public TypeOffPrice? TypeOffPrice { get; set; }
        public double? Value { get; set; }
    }
}
