using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Models.Interface
{
    public interface IStorage
    {
        //T GetList<T>() where T : IRepository;
        void Save();
    }
}
