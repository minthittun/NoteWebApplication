using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteWebApplication.Models;

namespace NoteWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private NoteWebDbContext db = new NoteWebDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult updateNote(Notes note)
        {
            Notes oldNoteModel = db.Notes.Find(note.NoteId);

            if(oldNoteModel == null)
            {
                //Add new
                note.NoteId = Guid.NewGuid();
                note.NotedDate = DateTime.UtcNow;
                db.Notes.Add(note);
                db.SaveChanges();
            }
            else
            {
                //Update
                note.NotedDate = DateTime.UtcNow;
                db.Entry(oldNoteModel).CurrentValues.SetValues(note);
                db.SaveChanges();
            }

            return Json(note);

        }

        [HttpGet]
        public IActionResult _note_list()
        {
            var result = db.Notes.OrderByDescending(n => n.NotedDate).AsQueryable();
            return PartialView(result);
        }

        [HttpGet]
        public JsonResult getNoteDetailById(string noteId)
        {
            var result = db.Notes.Find(new Guid(noteId));
            return Json(result);
        }

        [HttpGet]
        public JsonResult deleteNoteById(string noteId)
        {
            Notes note = db.Notes.Find(new Guid(noteId));
            db.Entry(note).State = EntityState.Deleted;
            db.SaveChanges();

            return Json("Successfully deleted.");
        }
    }
}
