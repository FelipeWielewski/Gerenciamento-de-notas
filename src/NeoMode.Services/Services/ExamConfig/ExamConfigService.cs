using Microsoft.EntityFrameworkCore;
using NeoMode.Core;
using NeoMode.Core.Domain.ExamConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public class ExamConfigService : IExamConfigService
    {
        private readonly ApplicationDbContext _dbContext;
        public ExamConfigService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ExamConfig> GetAll()
        {
            return _dbContext.ExamConfig.ToList();
        }
        public ExamConfig GetById(int Id)
        {
            return _dbContext.ExamConfig.Where(X => X.Id == Id).ToList().FirstOrDefault();
        }
        public ExamConfig GetConfigByDate(DateTime date)
        {
            return _dbContext.ExamConfig.Where(x => (x.ValidYearFrom.Date.Ticks <= date.Date.Ticks && x.ValidYearTo == null) || (x.ValidYearTo != null && x.ValidYearTo.Value.Date.Ticks >= date.Date.Ticks && x.ValidYearFrom.Date.Ticks <= date.Date.Ticks)).ToList().FirstOrDefault();
        }
        public void InsertExamConfig(ExamConfig ExamConfigToInsert)
        {
            var newExamConfig = new ExamConfig();

            newExamConfig.AverageScore = ExamConfigToInsert.AverageScore;
            newExamConfig.QuantityExam = ExamConfigToInsert.QuantityExam;
            newExamConfig.ValidYearFrom = ExamConfigToInsert.ValidYearFrom;
            newExamConfig.ValidYearTo = ExamConfigToInsert.ValidYearTo;
            newExamConfig.Id = 0;

            //Salvando
            _dbContext.ExamConfig.Add(newExamConfig);

            _dbContext.SaveChanges();
        }
        public bool UpdateExamConfig(ExamConfig ExamConfigToUpdate)
        {
            try
            {
                var old = GetById(ExamConfigToUpdate.Id);
                old.AverageScore = ExamConfigToUpdate.AverageScore;
                old.QuantityExam = ExamConfigToUpdate.QuantityExam;
                old.ValidYearFrom = ExamConfigToUpdate.ValidYearFrom;
                old.ValidYearTo = ExamConfigToUpdate.ValidYearTo;

                _dbContext.Entry(old).State = EntityState.Modified;

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
