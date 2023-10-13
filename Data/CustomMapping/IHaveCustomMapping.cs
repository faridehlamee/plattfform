using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
