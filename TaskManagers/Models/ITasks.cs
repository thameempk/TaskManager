namespace TaskManagers.Models
{
    public interface ITasks
    {
        public List<Tasks> GetTasks();
        public Tasks GetTaskById(int Task_id);
        public void CreateTask(Tasks task);
        public void UpdateTask(int id , Tasks task);
        public void DeleteTask(int Task_id);


    }

    public class TaskContext : ITasks
    {
        List<Tasks> tasks = new List<Tasks>
        {
            new Tasks {Task_Id = 1, Name = "learn .net core", Description = "learn .net core" , Status = "completed"},
             new Tasks {Task_Id = 2, Name = "learn .net core", Description = "learn .net core" , Status = "incompleted"},
        };

        public List<Tasks> GetTasks()
        {
            return tasks;
        }

        public Tasks GetTaskById(int Task_id)
        {
            var task = tasks.FirstOrDefault(s => s.Task_Id == Task_id);
            return task;
        }

        public void CreateTask (Tasks task)
        {
            tasks.Add(task);
        }

        public void UpdateTask (int id, Tasks task)
        {
            var item = tasks.FirstOrDefault(s => s.Task_Id == id);
            item.Name = task.Name;
            item.Description = task.Description;
            item.Status = task.Status;
        }

        public void DeleteTask(int Task_id)
        {
            var task = tasks.FirstOrDefault(s=> s.Task_Id == Task_id);
            tasks.Remove(task);

        }
    }

    
}
