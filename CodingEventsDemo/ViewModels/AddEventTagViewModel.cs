using CodingEventsDemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEventsDemo.ViewModels
{
    public class AddEventTagViewModel
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int TagId { get; set; }
        public Event Event { get; set; }
        public List<SelectListItem> Tags { get; set; }


        public AddEventTagViewModel()
        {
        }

        public AddEventTagViewModel(Event theEvent, List<Tag> possibleTags)
        {
            Tags = new List<SelectListItem>();
            foreach(var tag in possibleTags)
            {
                Tags.Add(new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                });
            }

            Event = theEvent;
        }
    }
}
