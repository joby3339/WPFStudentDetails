using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using WPFStudentDetails.Models;

namespace WPFStudentDetails.ViewModels
{
    public class StudentViewModel
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        public StudentViewModel()
        {
            LoadStudents();
        }
        // Loading student details from a JSON file
        private void LoadStudents()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var jsonPath = Path.Combine(baseDir, "Students.json");

            if (!File.Exists(jsonPath))
            {
                // Fallback: try project root during dev/run configurations
                var altPath = Path.Combine(Directory.GetCurrentDirectory(), "Students.json");
                jsonPath = File.Exists(altPath) ? altPath : jsonPath;
            }

            if (File.Exists(jsonPath))
            {
                var jsonData = File.ReadAllText(jsonPath);
                var studentList = JsonConvert.DeserializeObject<List<Student>>(jsonData) ?? new List<Student>();
                Students = new ObservableCollection<Student>(studentList);
            }
            else
            {
                Students = new ObservableCollection<Student>();
            }
        }
    }
}
