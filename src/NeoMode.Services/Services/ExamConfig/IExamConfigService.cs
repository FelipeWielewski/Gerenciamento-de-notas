using NeoMode.Core.Domain.ExamConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public interface IExamConfigService
    {
        IEnumerable<ExamConfig> GetAll();
        ExamConfig GetById(int Id);
        void InsertExamConfig(ExamConfig ExamConfigToInsert);
        bool UpdateExamConfig(ExamConfig ExamConfigToUpdate);
        ExamConfig GetConfigByDate(DateTime date);
    }
}
