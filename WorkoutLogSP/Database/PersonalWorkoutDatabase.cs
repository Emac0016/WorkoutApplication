using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogSP.ConstantVariables;
using WorkoutLogSP.ViewModels;

namespace WorkoutLogSP.Database
{
    class PersonalWorkoutDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<PersonalWorkoutDatabase> Instance = new AsyncLazy<PersonalWorkoutDatabase>(async () =>
        {
            var instance = new PersonalWorkoutDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Workouts>();
            return instance;
        });

        public PersonalWorkoutDatabase()
        {
            Database = new SQLiteAsyncConnection(SQLFunctionality.DatabasePath, SQLFunctionality.Flags);
        }

        public Task<List<Workouts>> GetItemsAsync()
        {
            return Database.Table<Workouts>().ToListAsync();
        }

        public Task<List<Workouts>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Workouts>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Workouts> GetItemAsync(int id)
        {
            return Database.Table<Workouts>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Workouts item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Workouts item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
