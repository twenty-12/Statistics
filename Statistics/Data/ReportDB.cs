using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Statistics.Models;
using System.Threading.Tasks;

namespace Statistics.Data
{
    public class ReportDB
    {
        readonly SQLiteAsyncConnection db;
        private List<Categories> BaseCategory =  new List<Categories>() { 
                
                new Categories()
                {
                    Category = "Заработная плата",
                    Operations = Operations.plus,
                                    },
                new Categories()
                {
                    Category = "Сдача недвижимости в аренду",
                    Operations = Operations.plus,
                                    },
                new Categories()
                {
                    Category = "Другое",
                    Operations = Operations.plus,
                                    },                 
                new Categories()
                {
                    Category = "Продукты питания",
                    Operations = Operations.minus,
                                    },
                new Categories()
                {
                    Category = "Транспорт",
                    Operations = Operations.minus,
                                    },
                new Categories()
                {
                    Category = "Мобильная связь",
                    Operations = Operations.minus,
                                    },
                new Categories()
                {
                    Category = "Интернет",
                    Operations = Operations.minus,
                                    },
                new Categories()
                {
                    Category = "Развлечения",
                    Operations = Operations.minus,
                                    },
                new Categories()
                {
                    Category = "Другое",
                    Operations = Operations.minus,
                                    }

        };
        public ReportDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);
            db.CreateTablesAsync<Report, Categories>().Wait();
            db.DeleteAllAsync<Categories>().Wait();
            foreach (Categories c in BaseCategory) {
                db.InsertAsync(c).Wait();
            }
        }

        public Task<List<Report>> GetReportsAsync()
        {
            return db.Table<Report>().ToListAsync();
        }

        public Task<List<Categories>> GetCategoriesAsync()
        {
            return db.Table<Categories>().ToListAsync();
        }

        public Task<List<Categories>> GetCategoriesAsync(Operations operation)
        {
            return db.Table<Categories>().Where(r => r.Operations == operation).ToListAsync();
        }

        public Task<List<Report>> GetReportsAsync(Operations operation, string Params)
        {
            switch (Params)
            {
                case "Порядок": return db.Table<Report>().Where(r => r.Operation == operation).OrderBy(R => R.Id).ToListAsync(); 
                case "Дата": return db.Table<Report>().Where(r => r.Operation == operation).OrderBy(R => R.Date).ToListAsync();
                case "Сумма": return db.Table<Report>().Where(r => r.Operation == operation).OrderByDescending(R => R.Summ).ToListAsync();
                
            }
            return db.Table<Report>().Where(r => r.Operation == operation).ToListAsync();
        }
        public Task<Categories> GetCategoryAsync(string name)
        {
            return db.Table<Categories>().Where(c => c.Category == name).FirstOrDefaultAsync();
        }
        public Task<List<Report>> GetReportsAsync(Operations operation)
        {
            return db.Table<Report>().Where(r => r.Operation == operation).ToListAsync();
        }
        public Task<Report> GetReportAsync(int id)
        {
            return db.Table<Report>().Where(n => n.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveReportAsync(Report report)
        {
            if (report.Id != 0)
            {
                return db.UpdateAsync(report);
            }
            else
            {
                return db.InsertAsync(report);
            }
        }

        public Task<int> SaveCategoryAsync(Categories category)
        {
            if (category.Id != 0)
            {
                return db.UpdateAsync(category);
            }
            else
            {
                return db.InsertAsync(category);
            }
        }

        public Task<int> DeleteReportAsync(Report report)
        {
            return db.DeleteAsync(report);
        }
    }
}
