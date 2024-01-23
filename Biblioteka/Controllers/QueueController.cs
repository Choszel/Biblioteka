using Biblioteka.Context;
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
        private readonly BibContext _context;

        public QueueController(BibContext context)
        {
            _context = context;
        }

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
                    queues[queue.BookId] = new List<PseudoQueue>();
                }
                var user = _context.Readers.FirstOrDefault(r => r.email == User.Identity.Name);
                PseudoQueue newItem = new(user.id, queue.Quantity);
                queues[queue.BookId].Add(newItem);

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
                    var queueList = queues[id];

                    var user = _context.Readers.FirstOrDefault(r => r.email == User.Identity.Name);
                    List<PseudoQueue> itemsToDelete = new();
                    if (user != null)
                        itemsToDelete = queueList.Where(item => item.UserId == user.id).ToList();
                    /*else
                        itemsToDelete = queueList.Where(item => item.UserId == -1).ToList();*/

                    foreach (var itemToDelete in itemsToDelete)
                    {
                        queueList.Remove(itemToDelete);
                    }

                    if (queueList.Count == 0)
                    {
                        queues.Remove(id);
                    }

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


        private Dictionary<int, List<PseudoQueue>> LoadQueues()
        {
            try
            {
                var json = System.IO.File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<Dictionary<int, List<PseudoQueue>>>(json) ?? new Dictionary<int, List<PseudoQueue>>();
            }
            catch
            {
                return new Dictionary<int, List<PseudoQueue>>();
            }
        }

        private void SaveQueues(Dictionary<int, List<PseudoQueue>> queues)
        {
            var json = JsonConvert.SerializeObject(queues);
            System.IO.File.WriteAllText(_filePath, json);
        }       
    }
}
