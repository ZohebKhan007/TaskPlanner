using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.MVVM.Models;

namespace TaskPlanner.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModels
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModels() 
        {
            FillData();
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        private void Tasks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void FillData()
        {
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Id = 1,
                    CategoryName = ".NET MAUI Course",
                    Color = "#CF14DF"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Tutorials",
                    Color = "#df6f14"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Shopping",
                    Color = "#14df80"
                }
            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask
                {
                    TaskName = "Upload Exersize Files",
                    Completed = false,
                    CategoryID = 1
                },
                new MyTask
                {
                    TaskName = "Plan Next Course",
                    Completed = false,
                    CategoryID = 1
                },
                new MyTask
                {
                    TaskName = "Upload New ASP.NET Video On YouTube",
                    Completed = false,
                    CategoryID = 2
                },
                new MyTask
                {
                    TaskName = "Fix Settings.cs class of the project",
                    Completed = false,
                    CategoryID = 2
                },
                new MyTask
                {
                    TaskName = "Update Github Repository",
                    Completed = true,
                    CategoryID = 2
                },
                new MyTask
                {
                    TaskName = "Buy Eggs",
                    Completed = false,
                    CategoryID = 3
                },
                new MyTask
                {
                    TaskName = "Go For The Pepperoni Pizza",
                    Completed = false,
                    CategoryID = 3
                },
            };

            UpdateData();
        }

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CategoryID == c.Id
                            select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                var notCompleted = from t in tasks
                                   where t.Completed == false
                                   select t;

                c.PendingTasks = notCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }
            foreach (var t in Tasks)
            {
                var catColor =
                    (from c in Categories
                     where c.Id == t.CategoryID
                     select c.Color).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }
    }
}
