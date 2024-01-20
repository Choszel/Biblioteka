using Biblioteka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Biblioteka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : Controller
    {
        private string _filePath = "../Biblioteka/wwwroot/rentQueue.json"; // ścieżka do pliku rentqueue.json       

        [HttpGet]
        public IActionResult GetJsonContent()
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(_filePath);
                return Content(jsonData, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd odczytu pliku JSON: {ex.Message}");
            }
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

        [HttpPatch("{id}")]
        public IActionResult UpdateQueue(int id, [FromBody] Queue updatedQueue)
        {
            try
            {
                var queues = LoadQueues();

                if (queues.ContainsKey(id))
                {
                    // Pobierz oryginalną kolejkę
                    var originalQueue = queues[id];


                    if (updatedQueue.Quantity != 0)
                        originalQueue[0].Quantity = updatedQueue.Quantity;

                    // Zapisz zaktualizowane kolejki
                    SaveQueues(queues);

                    return Json(new { success = true, message = "Queue updated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Queue not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFromQueue(int id)
        {
            try
            {
                var queues = LoadQueues();

                if (queues.ContainsKey(id))
                {
                    // Pobierz oryginalną kolejkę
                    var queueList = queues[id];


                    var itemsToDelete = queueList.Where(item => item.Quantity == 6).ToList();

                    // Usuń znalezione elementy
                    foreach (var itemToDelete in itemsToDelete)
                    {
                        queueList.Remove(itemToDelete);
                    }

                    // Jeśli lista jest teraz pusta, usuń klucz
                    if (queueList.Count == 0)
                    {
                        queues.Remove(id);
                    }

                    // Zapisz zaktualizowane kolejki
                    SaveQueues(queues);

                    return Json(new { success = true, message = "Queue updated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Queue not found." });
                }
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
