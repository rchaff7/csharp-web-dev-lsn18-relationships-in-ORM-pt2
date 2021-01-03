using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEventsDemo.Controllers
{
    public class TagController : Controller
    {
        private EventDbContext context;
        public TagController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Tag> tags = context.Tags.ToList();
            return View(tags);
        }

        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if (ModelState.IsValid)
            {
                context.Tags.Add(tag);
                context.SaveChanges();
                return Redirect("/Tag");
            }

            return View("Add", tag);
        }

        // responds to URLs like /Tag/AddEvent/5 (where 5 is an event ID)
        public IActionResult AddEvent(int id)
        {
            Event theEvent = context.Events.Find(id);
            List<Tag> possibleTags = context.Tags.ToList();

            AddEventTagViewModel viewmodel = new AddEventTagViewModel(theEvent, possibleTags);
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult AddEvent(AddEventTagViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                EventTag eventTag = new EventTag
                {
                    EventId = viewModel.EventId,
                    TagId = viewModel.TagId
                };
                context.EventTags.Add(eventTag);
                context.SaveChanges();

                return Redirect("/Events/Detail/" + viewModel.EventId);
            }

            return View(viewModel);
        }
    }
}
