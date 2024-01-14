using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Biblioteka.Controllers
{
    public class QueueController : Controller
    {
        private string _filePath = "rentQueue.json"; // ścieżka do pliku rentqueue.json

        public IActionResult Index()
        {
            var queues = LoadQueues();
            return View(queues);
        }

        [HttpPost]
        public IActionResult AddToQueue([FromBody] Queue queue)
        {
            try
            {
                var queues = LoadQueues();

                if (!queues.ContainsKey(queue.BookId))
                {
                    queues[queue.BookId] = new List<Queue>();
                }

                queues[queue.BookId].Add(queue);

                SaveQueues(queues);

                return Json(new { success = true, message = "Product added to the queue successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        private Dictionary<int, List<Queue>> LoadQueues()
        {
            try
            {
                var json = System.IO.File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<Dictionary<int, List<Queue>>>(json) ?? new Dictionary<int, List<Queue>>();
            }
            catch
            {
                return new Dictionary<int, List<Queue>>();
            }
        }

        private void SaveQueues(Dictionary<int, List<Queue>> queues)
        {
            var json = JsonConvert.SerializeObject(queues);
            System.IO.File.WriteAllText(_filePath, json);
        }
    }
}
