using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ежедневник.Models;

namespace Ежедневник.Services
{
    internal class FileIOServices
    {
        private readonly string PATH;

        public FileIOServices(string path)
        {
            PATH = path;
        }

        public BindingList<ToDoModel> LoadData()
        {
            var fileExist = File.Exists(PATH);
            if (!fileExist)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToDoModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }
        public void SaveData(object _todoData)
        {
            using (StreamWriter writer = File.CreateText(PATH)) 
            {
                string output = JsonConvert.SerializeObject(_todoData);
                writer.Write(output);
            }
        }
    }
}
